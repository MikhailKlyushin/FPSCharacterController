using UnityEngine;

public class CharacterCameraView : MonoBehaviour
{
    private Transform _targetForCamera;
    private CharacterModel _model;

    private const float _smoothSpeed = 0.15f;

    public void SetModel(CharacterModel model)
    {
        _model = model;
    }

    public void SetTarget(Transform target)
    {
        _targetForCamera = target;
    }

    private void FixedUpdate()
    {
        Vector3 Position = _targetForCamera.transform.position;
        Vector3 SmoothVector = Vector3.Lerp(transform.position, Position, _smoothSpeed);
        transform.position = SmoothVector;

        transform.localEulerAngles = _model.LocalRotate;
    }

}
