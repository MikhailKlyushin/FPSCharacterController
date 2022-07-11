using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CharacterCameraView : MonoBehaviour
{
    private float _rotationPositionX;
    private float _rotationPositionY;

    private Transform _targetForCamera;
    private Vector3 _localRotate;

    private CameraConfig _config;

    [Inject]
    private void Construct(SignalBus signalBus, CameraConfig config)
    {
        signalBus.Subscribe<ISignalInput>(input => ChangeCharacterPosition(input.PositionToMove, input.PositionToRotate));
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
            transform.localEulerAngles = _localRotate;
        }
        else
            transform.localEulerAngles = _localRotate;
    }


    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _localRotate = RotateToPosition(positionToRotate);
    }

    private Vector3 RotateToPosition(Vector3 positionToRotate)
    {
        _rotationPositionX -= positionToRotate.x * _config.SensivityVertical;
        _rotationPositionX = Mathf.Clamp(_rotationPositionX, _config.MinimumVerticalAngle, _config.MaximumVerticalAngle);

        var delta = positionToRotate.y * _config.SensivityHorizontal;
        _rotationPositionY += delta;


        var localRotate = new Vector3(_rotationPositionX, _rotationPositionY, 0);
        return localRotate;
    }
}