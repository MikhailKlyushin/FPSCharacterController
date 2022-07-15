using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

public class CharacterCameraView : MonoBehaviour
{
    private float _verticalRotateByX;
    private float _horizontalRotateByY;
    
    private Vector3 _rotationVector;
    private Transform _targetForCamera;
    private IInputProvider _input;
    private CameraConfig _config;
    
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    [Inject]
    private void Construct(IInputProvider input, CameraConfig config)
    {
        _input = input;
        _config = config;
    }

    private void Start()
    {
        _input.RotatePosition.Subscribe(ChangeCameraPosition).AddTo(_disposable);
    }

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    private void ChangeCameraPosition(Vector3 positionToRotate)
    {
        var position = _targetForCamera.transform.position;
        var smoothVector = Vector3.Lerp(transform.position, position, _config.SmoothSpeed);
        transform.position = smoothVector;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, RotateToPosition(positionToRotate), _config.SmoothRotate);
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