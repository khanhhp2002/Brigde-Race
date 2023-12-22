using UnityEngine;

public class Player : Character
{
    [SerializeField] private JoyStick _joyStick;
    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (_isFreeze) return;
        Vector3 direction = GetJoyStickData();
        if (direction != Vector3.zero)
        {
            transform.position += direction * _moveSpeed * Time.fixedDeltaTime;
        }
        LookToTarget(transform.position + direction);
    }

    private Vector3 GetJoyStickData()
    {
        Vector3 joyStickDir = _joyStick.GetDirection();
        if (joyStickDir.y > 0 && _isBlocked)
        {
            return new Vector3(joyStickDir.x, 0f, 0f);
        }
        return new Vector3(joyStickDir.x, 0f, joyStickDir.y);
    }
}
