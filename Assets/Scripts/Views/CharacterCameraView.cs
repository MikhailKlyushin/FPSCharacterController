using UniRx;
using UnityEngine;
using Zenject;

public class CharacterCameraView : MonoBehaviour
{
    private float _verticalRotateByX;
    private Vector3 _rotationVector;
    private Vector3 _positionToRotateVector3;
    
    private Transform _targetForCamera;
    private IInputProvider _input;
    private CameraConfig _config;
    
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    [Inject]
    private void Construct(IInputProvider input, CameraConfig config)
    {
        _input = input;
        _config = config;
    }
    

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    private void Start()
    {
        _input.RotatePosition.Subscribe(ChangeCameraPosition).AddTo(_disposables);
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            var position = _targetForCamera.transform.position;
            var smoothVector = Vector3.Lerp(transform.position, position, _config.SmoothSpeed);
            transform.position = smoothVector;

            transform.rotation = Quaternion.Lerp(transform.rotation, 
                VerticalRotateToPosition(_positionToRotateVector3), _config.SmoothRotate);
        }).AddTo(_disposables);
    }

    private void ChangeCameraPosition(Vector2 positionToRotate)
    {
        _positionToRotateVector3 = new Vector3(positionToRotate.y, positionToRotate.x, 0);
    }

    private Quaternion VerticalRotateToPosition(Vector3 positionToRotate)
    {
        _verticalRotateByX -= positionToRotate.x * _config.SensitivityVertical * Time.deltaTime;
        _verticalRotateByX = Mathf.Clamp(  _verticalRotateByX, 
            _config.MinimumVerticalAngle, _config.MaximumVerticalAngle);
        _rotationVector.x = _verticalRotateByX;

        var cameraRotate = _targetForCamera.transform.rotation;
        return  cameraRotate *= Quaternion.Euler(_rotationVector);
    }
}