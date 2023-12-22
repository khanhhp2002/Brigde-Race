using UnityEngine;

public class GroundBrick : Brick
{
    [SerializeField] private float _activateTime = 5f;

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Invoke(nameof(Activate), _activateTime);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
