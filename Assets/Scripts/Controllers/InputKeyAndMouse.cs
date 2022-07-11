using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class InputKeyAndMouse : IInputProvider, ITickable
{
    private float _horizontalPosition;
    private float _verticalPosition;
    private Vector3 _positionToMove;

    private float _rotationX;
    private float _rotationY;
    private Vector3 _positionToRotate;

    private float _blindSpot = 0.2f;

    public event IInputProvider.InputHandler InputNotify;

    /*public InputKeyAndMouse()
    {
        //this.UpdateInput();
    }*/
    
    public void Tick()
    {
        _horizontalPosition = Input.GetAxis("Horizontal");
        _verticalPosition = Input.GetAxis("Vertical");

        _rotationX = Input.GetAxis("Mouse Y");
        _rotationY = Input.GetAxis("Mouse X");


        SetMoveAndRotatePosition();
    }
    /*async public void UpdateInput()
    {
        while (true)
        {
            _horizontalPosition = Input.GetAxis("Horizontal");
            _verticalPosition = Input.GetAxis("Vertical");

            _rotationX = Input.GetAxis("Mouse Y");
            _rotationY = Input.GetAxis("Mouse X");


            SetMoveAndRotatePosition();

            await Task.Delay(20);
        }
    }*/

    private void SetMoveAndRotatePosition()
    {
        if ((_horizontalPosition >= _blindSpot) || (_horizontalPosition <= _blindSpot) || 
            (_verticalPosition >= _blindSpot) || (_verticalPosition <= _blindSpot))
        {
            _positionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
            _positionToRotate = new Vector3(_rotationX, _rotationY, 0);
            InputNotify?.Invoke(_positionToMove, _positionToRotate);
        }
        else
        {
            _positionToMove = new Vector3(0, 0, 0);
            _positionToRotate = new Vector3(_rotationX, _rotationY, 0);
            InputNotify?.Invoke(_positionToMove, _positionToRotate);
        }
    }
}
