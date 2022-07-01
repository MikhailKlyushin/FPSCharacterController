using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyProvider
{
    private Vector3 _positionToMove;
    private float _horizontalPosition;
    private float _verticalPosition;

    public Vector3 GetMovePosition()
    {
        UpdatePositionToMove();
        return _positionToMove;
    }

    private void UpdatePositionToMove()
    {
        TrackKeyDown();
        ConvertPositionInVector3();
    }

    private void TrackKeyDown()
    {
        _horizontalPosition = Input.GetAxis("Horizontal");
        _verticalPosition = Input.GetAxis("Vertical");
    }

    private void ConvertPositionInVector3()
    {
        _positionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
    }
}
