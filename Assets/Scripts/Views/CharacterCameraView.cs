using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

public class CharacterCameraView : MonoBehaviour
{
    //TODO: Delete comments
    private float _verticalRotateByX;
    //private float _horizontalRotateByY;
    
    private Vector3 _rotationVector;
    private Vector3 _positionToRotateVector3;
    private Transform _targetForCamera;
    private IInputProvider _input;
    private CameraConfig _config;

    //TODO: You dont dispose streams!!! Use .AddTo(this) - this dispose the stream after destroy GO
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    [Inject]
    private void Construct(IInputProvider input, CameraConfig config)
    {
        _input = input;
        _config = config;
    }

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    private void Start()
    {
        _input.RotatePosition.Subscribe(ChangeCameraPosition).AddTo(_disposable);
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            var position = _targetForCamera.transform.position;
            var smoothVector = Vector3.Lerp(transform.position, position, _config.SmoothSpeed);
            transform.position = smoothVector;

            transform.rotation = Quaternion.Lerp(transform.rotation, RotateToPosition(_positionToRotateVector3), _config.SmoothRotate);
        }).AddTo(_disposable);
    }

    private void ChangeCameraPosition(Vector2 positionToRotate)
    {
        _positionToRotateVector3 = new Vector3(positionToRotate.y, positionToRotate.x, 0);
    }

    private Quaternion RotateToPosition(Vector3 positionToRotate)
    {
        _verticalRotateByX -= positionToRotate.x * _config.SensitivityVertical * Time.deltaTime;
        _verticalRotateByX = Mathf.Clamp(_verticalRotateByX, _config.MinimumVerticalAngle, _config.MaximumVerticalAngle);
        
        //var delta = positionToRotate.y * _config.SensitivityHorizontal * Time.deltaTime;
        //_horizontalRotateByY += delta;
        
        _rotationVector.x = _verticalRotateByX;
        //_rotationVector.y = _horizontalRotateByY;
        var cameraRotate = _targetForCamera.transform.rotation;
        return  cameraRotate *= Quaternion.Euler(_rotationVector);
    }
}