using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    public float MoveSpeed;
    private Rigidbody _rigidbody;

    private Vector3 _positionCameraView => _camera.forward * _distanceOffsetCamera;
    private Vector3 _positionToMove;
    private Quaternion _rotationAngle;

    private float _horizontalPosition;
    private float _verticalPosition;

    private const float _distanceOffsetCamera = 5f;
    private const float _angularSpeedCharacter = 400f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        RotateToCameraPoint();
    }

    void FixedUpdate()
    {
        MoveToNormalizePosition();
    }

    private void MoveToNormalizePosition()
    {
        _horizontalPosition = Input.GetAxis("Horizontal");
        _verticalPosition = Input.GetAxis("Vertical");
        _positionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
        _positionToMove = transform.TransformDirection(_positionToMove);
        Vector3.Normalize(_positionToMove);
        _rigidbody.velocity = _positionToMove * MoveSpeed;
    }

    private void RotateToCameraPoint()
    {
        Vector3 tartget = _positionCameraView;
        tartget.y = 0;

        _rotationAngle = Quaternion.LookRotation(tartget);
        float speedRotation = _angularSpeedCharacter * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationAngle, speedRotation);
    }
}
