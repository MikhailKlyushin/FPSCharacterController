using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : MonoBehaviour
{
    public float SensivityHorizontal = 9f;
    public float SensivityVertical = 9f;

    public float MinimumVertical = -45f;
    public float MaximumVertical = 45f;

    private readonly InputCameraProvider _inputCameraProvider = new InputCameraProvider();
    private Vector3 _positionToRotate;

    private void Update()
    {
        _positionToRotate = _inputCameraProvider.GetRotatePosition();
        _positionToRotate = AddRotationSensivity(_positionToRotate);
        transform.localEulerAngles = _positionToRotate;
    }

    private Vector3 AddRotationSensivity(Vector3 positionToRotate)
    { 
        var rotationVertical = positionToRotate.x * SensivityVertical;
        rotationVertical = Mathf.Clamp(rotationVertical, MinimumVertical, MaximumVertical);

        var deltaSensivity = positionToRotate.y * SensivityHorizontal;
        var rotationHorizontal = transform.localEulerAngles.y + deltaSensivity;

        return new Vector3(rotationVertical, rotationHorizontal, 0);
    }
}
