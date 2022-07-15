using DG.Tweening;
using UnityEngine;
using Zenject;

public class CharacterCameraView : MonoBehaviour
{
    private float _verticalRotateByX;
    private float _horizontalRotateByY;
    
    private Vector3 _rotationVector;
    private Transform _targetForCamera;
    private CameraConfig _config;

    [Inject]
    private void Construct(SignalBus signalBus, CameraConfig config)
    {
        signalBus.Subscribe<SignalInputProvider>(input => ChangeCameraPosition(input.PositionToMove, input.PositionToRotate));
        _config = config;
    }

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    private void ChangeCameraPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        var position = _targetForCamera.transform.position;
        var smoothVector = Vector3.Lerp(transform.position, position, _config.SmoothSpeed);
        transform.position = smoothVector;
        
        //TODO:Add parameter t to config like for SmoothSpeed
        transform.rotation =Quaternion.Lerp(transform.rotation, RotateToPosition(positionToRotate), 0.8f);
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