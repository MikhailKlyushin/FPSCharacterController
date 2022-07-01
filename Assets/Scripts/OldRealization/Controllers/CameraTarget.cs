using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _targetForCamera;

    private const float _distanceOffsetCamera = 5f;
    private const float _angularSpeedCharacter = 400f;

    private Quaternion _rotationAngle;
    private Vector3 _positionCameraView => _camera.forward * _distanceOffsetCamera;

    private void Update()
    {
        _camera.transform.position = _targetForCamera.transform.position;
        RotationTarget();
    }

    private void RotationTarget()
    {
        Vector3 tartget = _positionCameraView;
        tartget.y = 0;

        _rotationAngle = Quaternion.LookRotation(tartget);
        float speedRotation = _angularSpeedCharacter * Time.deltaTime;
        _targetForCamera.transform.rotation = Quaternion.RotateTowards(_targetForCamera.transform.rotation, _rotationAngle, speedRotation);
    }
}
