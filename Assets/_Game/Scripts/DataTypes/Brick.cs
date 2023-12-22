using System;
using UnityEngine;

public class Brick : MonoBehaviour, IPoolable<Brick>
{
    [SerializeField] protected ColorType _colorType;
    [SerializeField] protected MeshRenderer _meshRenderer;
    protected Action<Brick> _returnAction;

    public ColorType ColorType => _colorType;

    public void ChangeColor(ColorType colorType)
    {
        _meshRenderer.material = GameplayManager.Instance.GetMaterialByColorType(colorType);
        _colorType = colorType;
    }

    public void Initialize(Action<Brick> returnAction)
    {
        this._returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        _returnAction?.Invoke(this);
    }
}
