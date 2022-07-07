using UnityEngine;
using UnityEngine.Serialization;

public class CharacterCameraView : MonoBehaviour
{
    #region Params

    [SerializeField] private float sensivityHorisontal = 20f;
    [SerializeField] private float sensivityVertical = 20f;

    [SerializeField] private float minimumVerticalAngle = -45f;
    [SerializeField] private float maximumVerticalAngle = 45f;

    private float _rotationPositionX;
    private float _rotationPositionY;

    private const float SmoothSpeed = 0.15f;

    #endregion

    private Transform _targetForCamera;
    private Vector3 _localRotate;
    

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    public void SetInput(IInputProvider input)
    {
        input.InputNotify += ChangeCharacterPosition;
    }

    private void FixedUpdate()
    {
        if (_targetForCamera != null)
        {
            Vector3 position = _targetForCamera.transform.position;
            Vector3 smoothVector = Vector3.Lerp(transform.position, position, SmoothSpeed);
            transform.position = smoothVector;

            //transform.localEulerAngles = _model.LocalRotate;
            transform.localEulerAngles = _localRotate;
        }
        // else
        //     transform.localEulerAngles = _localRotate;
    }


    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _localRotate = RotateToPosition(positionToRotate);
    }

    private Vector3 RotateToPosition(Vector3 positionToRotate)
    {
        _rotationPositionX -= positionToRotate.x * sensivityVertical;
        _rotationPositionX = Mathf.Clamp(_rotationPositionX, minimumVerticalAngle, maximumVerticalAngle);

        var delta = positionToRotate.y * sensivityHorisontal;
        _rotationPositionY += delta;


        var localRotate = new Vector3(_rotationPositionX, _rotationPositionY, 0);
        return localRotate;
    }
}