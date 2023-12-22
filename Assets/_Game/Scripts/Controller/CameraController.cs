using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed = 0.125f;

    private void Start()
    {
        //_offset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _smoothSpeed * Time.fixedDeltaTime);
        //transform.position = _target.position + _offset;
    }
}
