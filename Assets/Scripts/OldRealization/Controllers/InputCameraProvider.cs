using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCameraProvider
{
    private Vector3 _positionToRotate;

    private float _rotationHorizontal;
    private float _rotationVertical;

    public Vector3 GetRotatePosition()
    {
        UpdatePositionToRotate();
        return _positionToRotate;
    }

    private void UpdatePositionToRotate()
    {
        TrackMouse();
        ConvertPositionInVector3();
    }

    private void TrackMouse()
    {
        _rotationHorizontal = Input.GetAxis("Mouse X");
        _rotationVertical -= Input.GetAxis("Mouse Y");
    }

    private void ConvertPositionInVector3()
    {
        _positionToRotate = new Vector3(_rotationVertical, _rotationHorizontal, 0);
    }
}
