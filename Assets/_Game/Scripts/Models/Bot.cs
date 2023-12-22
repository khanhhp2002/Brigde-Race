using UnityEngine;

public class Bot : Character
{
    private IState _currentState;
    private Vector3? _targetBrick = null;
    private Vector3 _bridgeStartPos;
    private Vector3? _bridgeEndPos = null;

    public Vector3 TargetBrick { get => _targetBrick.Value; }
    public Vector3 BridgeStartPos { get => _bridgeStartPos; }
    public Vector3 BridgeEndPos { get => _bridgeEndPos.Value; set => _bridgeEndPos = value; }
    protected override void Start()
    {
        base.Start();
        _currentState = new BrickFindingState();
    }

    void Update()
    {
        if (_isFreeze) return;
        _currentState.OnExecute(this);
    }

    public void ChangeState(IState state)
    {
        _currentState.OnExit(this);
        _currentState = state;
        _currentState.OnEnter(this);
    }
    public bool FindBrick(int attempt)
    {
        if (_targetBrick is not null) return true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereCastRadius * attempt, BitShift((byte)LayerMaskType.GroundBrick));
        if (colliders != null && colliders.Length > 0)
        {
            for (int i = colliders.Length - 1; i >= 0; i--)
            {
                if (colliders[i].GetComponent<GroundBrick>().ColorType == _colorType)
                {
                    Vector3 brickPos = colliders[i].transform.position;
                    _targetBrick = new Vector3(brickPos.x, transform.position.y, brickPos.z);
                    return true;
                }
            }
        }
        return false;
    }

    public bool MoveToBrick()
    {
        if (_targetBrick is null) return true;
        transform.position = Vector3.MoveTowards(this.transform.position, _targetBrick.Value, _moveSpeed * Time.deltaTime);
        return false;
    }

    public bool MoveToBridge()
    {
        if (transform.position == _bridgeStartPos)
        {
            return true;
        }
        transform.position = Vector3.MoveTowards(this.transform.position, _bridgeStartPos, _moveSpeed * Time.deltaTime);
        return false;
    }

    public bool TryMoveToEndBridge()
    {
        if (transform.position == _bridgeEndPos.Value)
        {
            _bridgeEndPos = null;
            return true;
        }
        transform.position = Vector3.MoveTowards(this.transform.position, _bridgeEndPos.Value, _moveSpeed * Time.deltaTime);
        return false;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.layer == (byte)LayerMaskType.GroundBrick)
        {
            if (other.GetComponent<GroundBrick>().ColorType == _colorType)
            {
                _targetBrick = null;
            }
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        if (other.gameObject.layer == (byte)LayerMaskType.Ground)
        {
            if (_bridgeController is null)
            {
                Debug.Log("???");
                _bridgeController = _phaseController.GetClosestBridge(transform.position);
                _bridgeStartPos = new Vector3(_bridgeController.StartPosition.x, 1.5f, _bridgeController.StartPosition.z);
                //_bridgeEndPos = new Vector3(_bridgeController.EndPosition.x, 1.5f, _bridgeController.EndPosition.z);
            }
        }
    }

}
