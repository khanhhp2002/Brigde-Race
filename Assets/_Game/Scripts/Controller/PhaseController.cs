using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    [SerializeField] private List<BridgeController> _phaseBridges;
    [SerializeField] private GroundBrick _groundBrickPrefab;
    [SerializeField] private Transform _groundBrickHolder;
    [SerializeField] private BridgeController[] _bridges;
    #region Spawn settings
    [SerializeField] private int _spawnHeight = 5;
    [SerializeField] private int _spawnWidth = 5;
    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField] private Vector3 _distanceBetweenBridges;
    #endregion
    private List<ColorType> _colorTypes = new List<ColorType>() { ColorType.Red, ColorType.Orange, ColorType.Blue, ColorType.Yellow };
    private List<GroundBrick> _groundBricks = new List<GroundBrick>();

    private void Start()
    {
        SpawnGroundBrick();
    }

    private void SpawnGroundBrick()
    {
        for (int i = _spawnHeight, k = 0; i >= -_spawnHeight; i--)
        {
            for (int j = _spawnWidth; j >= -_spawnWidth; j--, k++)
            {
                GroundBrick groundBrick = Instantiate(_groundBrickPrefab);
                groundBrick.transform.SetParent(_groundBrickHolder);
                groundBrick.transform.localPosition = new Vector3((i + _spawnOffset.x) * _distanceBetweenBridges.x, _spawnOffset.y, (j + _spawnOffset.z) * _distanceBetweenBridges.z);
                //k = k == _colorTypes.Count ? 0 : k;
                //groundBrick.ChangeColor(_colorTypes[k]);
                groundBrick.gameObject.SetActive(false);
                _groundBricks.Add(groundBrick);
            }
        }
        Disorder();
    }

    public void Disorder()
    {
        for (int i = _groundBricks.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = _groundBricks[i];
            _groundBricks[i] = _groundBricks[j];
            _groundBricks[j] = temp;
        }
        int k = 0;
        foreach (var brick in _groundBricks)
        {
            k = k == _colorTypes.Count ? 0 : k;
            brick.ChangeColor(_colorTypes[k]);
            k++;
        }
    }

    public void ShowAllBrick()
    {
        foreach (var brick in _groundBricks)
        {
            brick.gameObject.SetActive(true);
        }
    }

    public void ShowCustomBrick(ColorType colorType)
    {
        foreach (var brick in _groundBricks)
        {
            if (brick.ColorType == colorType)
            {
                brick.gameObject.SetActive(true);
            }
        }
    }

    public BridgeController GetClosestBridge(Vector3 characterPos)
    {
        var distance = float.MaxValue;
        BridgeController closestBridge = null;
        foreach (BridgeController bridge in _bridges)
        {
            var tmp = Mathf.Abs((characterPos - bridge.StartPosition).sqrMagnitude);
            if (tmp < distance)
            {
                distance = tmp;
                closestBridge = bridge;
            }
        }
        return closestBridge;
    }

    public void DestrouBrickByColor(ColorType colorType)
    {
        foreach (var brick in _groundBricks)
        {
            if (brick.ColorType == colorType)
            {
                Destroy(brick.gameObject);
            }
        }
    }
}
