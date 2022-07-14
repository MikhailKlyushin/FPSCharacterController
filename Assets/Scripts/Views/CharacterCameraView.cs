using UnityEngine;
using Zenject;

public class CharacterCameraView : MonoBehaviour
{
    private float _verticalRotateByX;
    private float _horizontalRotateByY;
    
    private Vector3 _rotationVector;
    private Quaternion _rotate;

    private Transform _targetForCamera;
    private CameraConfig _config;

    [Inject]
    private void Construct(SignalBus signalBus, CameraConfig config)
    {
        signalBus.Subscribe<SignalInputProvider>(input => ChangeCharacterPosition(input.PositionToMove, input.PositionToRotate));
        _config = config;
    }

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    private void FixedUpdate()
    {
        if (_targetForCamera != null)
        {
            Vector3 position = _targetForCamera.transform.position;
            Vector3 smoothVector = Vector3.Lerp(transform.position, position, _config.SmoothSpeed);
            transform.position = smoothVector;

            transform.rotation = Quaternion.Lerp(transform.rotation, _rotate, 0.5f); //TODO: add const in config
        }
    }


    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _rotate = RotateToPosition(positionToRotate);
    }

    private Quaternion RotateToPosition(Vector3 positionToRotate)
    {
        _verticalRotateByX -= positionToRotate.x * _config.SensitivityVertical * Time.deltaTime;
        _verticalRotateByX = Mathf.Clamp(_verticalRotateByX, _config.MinimumVerticalAngle, _config.MaximumVerticalAngle);
        
        var delta = positionToRotate.y * _config.SensitivityHorizontal * Time.deltaTime;
        _horizontalRotateByY += delta;

        _rotationVector.x = _verticalRotateByX;
        _rotationVector.y = _horizontalRotateByY;

        return Quaternion.Euler(_rotationVector);
    }
}