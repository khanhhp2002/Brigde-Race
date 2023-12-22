using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    private Dictionary<ColorType, int> _colorDistribution = new Dictionary<ColorType, int>();
    private int _currentLength = 0;
    private bool _isClosed = false;
    public Vector3 StartPosition => _startPosition.position;
    public Vector3 EndPosition => _endPosition.position;

    private void Start()
    {
        foreach (ColorType colorType in GameplayManager.Instance.GetColorTypes())
        {
            _colorDistribution.Add(colorType, 0);
        }
    }

    public void AddColor(ColorType colorType)
    {
        _colorDistribution[colorType]++;
        _currentLength++;
    }

    public void ChangeColor(ColorType oldColor, ColorType newColor)
    {
        _colorDistribution[oldColor]--;
        _colorDistribution[newColor]++;
    }

    public float GetPercentage(ColorType colorType)
    {
        return (float)_colorDistribution[colorType] / _currentLength;
    }
}
