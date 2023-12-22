using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    #region Fields
    [Header("Character Components"), Space(5f)]
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected CapsuleCollider _capsuleCollider;
    [SerializeField] protected MeshRenderer _meshRenderer;

    [Header("Character Stats"), Space(5f)]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _sphereCastRadius;
    [SerializeField] protected ColorType _colorType;
    [SerializeField] protected float _freezeTime = 5f;

    [Header("Character References"), Space(5f)]
    [SerializeField] protected HandBrick _handBrick;
    [SerializeField] protected GameObject _handBrickHolder;
    protected PhaseController _phaseController;
    protected BridgeController _bridgeController = null;
    protected static ObjectPool<Brick> _handBrickPool;
    protected Stack<Brick> _handBrickStack = new Stack<Brick>();
    protected Vector3 _holderPos = Vector3.zero;
    protected int _brickCount = 0;
    protected int _bridgeIndex = 1;
    protected bool _isBlocked = false;
    protected bool _isFreeze = false;
    protected Coroutine _stunCoroutine;

    #endregion

    #region Methods
    protected virtual void Start()
    {
        if (_handBrickPool == null)
            _handBrickPool = new ObjectPool<Brick>(_handBrick.gameObject);
    }
    public BridgeController BridgeController => _bridgeController;

    public int GetBrickCount() => _brickCount;

    public void ChangeColor(ColorType colorType)
    {
        _meshRenderer.material = GameplayManager.Instance.GetMaterialByColorType(colorType);
    }

    public void Stun()
    {
        if (_stunCoroutine is null)
        {
            _stunCoroutine = StartCoroutine(StunCo());
        }
    }

    IEnumerator StunCo()
    {
        int count = _handBrickStack.Count;
        ChangeColor(ColorType.Gray);
        FreezeCharacter(true);
        while (_handBrickStack.Count > 0)
        {
            RemoveHandBrick();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds((float)(_freezeTime - (count * 0.1)));
        FreezeCharacter(false);
        ChangeColor(_colorType);
        _stunCoroutine = null;
    }

    protected void AddHandBrick()
    {
        _brickCount++;
        Brick brick = _handBrickPool.Pull(_holderPos, _handBrickHolder.transform);
        brick.ChangeColor(_colorType);
        _holderPos += Vector3.up * 0.5f;
        _handBrickStack.Push(brick);
    }

    protected void RemoveHandBrick()
    {
        _brickCount--;
        _handBrickStack.Pop().ReturnToPool();
        _holderPos -= Vector3.up * 0.5f;
    }

    protected void FreezeCharacter(bool isFreeze)
    {
        _isFreeze = isFreeze;
        _rigidbody.isKinematic = isFreeze;
        _capsuleCollider.isTrigger = isFreeze;
    }

    protected int BitShift(byte layer) => 1 << layer;

    public void LookToTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    /// <summary>
    /// Handled the collision between the player and the ground brick.
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (byte)LayerMaskType.GroundBrick)
        {

            GroundBrick groundBrick = other.GetComponent<GroundBrick>();
            if (groundBrick.ColorType == _colorType)
            {
                groundBrick.Deactivate();
                AddHandBrick();
            }
        }
    }

    /// <summary>
    /// Handled the collision between the player and the stair brick.
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == (byte)LayerMaskType.StairBrick)
        {
            StairBrick stairBrick = other.gameObject.GetComponent<StairBrick>();
            if (stairBrick.ColorType != _colorType)
            {
                if (_brickCount > 0)
                {
                    stairBrick.ChangeColor(_colorType);
                    RemoveHandBrick();
                }
                else
                {
                    _isBlocked = true;
                }
            }
        }
        else if (other.gameObject.layer == (byte)LayerMaskType.Ground)
        {
            Ground ground = other.gameObject.GetComponent<Ground>();
            if (_phaseController is null || _phaseController != ground.PhaseController)
            {
                _bridgeController = null;
                _phaseController = ground.PhaseController;
                _phaseController.ShowCustomBrick(_colorType);
            }
        }
        else if (other.gameObject.layer == (byte)LayerMaskType.Character)
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (this._brickCount > character.GetBrickCount())
            {
                Debug.Log($"{this.gameObject.name} is stun {character.gameObject.name}");
                character.Stun();
            }
        }
    }

    /// <summary>
    /// Handled the collision between the player and the stair brick.
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == (byte)LayerMaskType.StairBrick)
        {
            _isBlocked = false;
        }
    }
    #endregion
}
