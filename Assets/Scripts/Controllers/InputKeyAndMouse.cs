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

    private InputConfig _config;

    private InputKeyAndMouse(InputConfig config)
    {
        _config = config;
    }

    public event IInputProvider.InputHandler InputNotify;

    public void Tick()
    {
        _horizontalPosition = Input.GetAxis("Horizontal");
        _verticalPosition = Input.GetAxis("Vertical");

        _rotationX = Input.GetAxis("Mouse Y");
        _rotationY = Input.GetAxis("Mouse X");
        
        SetMoveAndRotatePosition();
    }
    private void SetMoveAndRotatePosition()
    {
        _positionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
        _positionToRotate = new Vector3(_rotationX, _rotationY, 0);
        InputNotify?.Invoke(_positionToMove, _positionToRotate);
    }
}
