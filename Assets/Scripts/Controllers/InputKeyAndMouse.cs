using System.Threading.Tasks;
using UnityEngine;

public class InputKeyAndMouse : IInputProvider
{
    private float _horizontalPosition;
    private float _verticalPosition;
    private Vector3 _positionToMove;

    private float _rotationPositionX;
    private float _rotationPositionY;
    private Vector3 _positionToRotate;



    public event IInputProvider.InputHandler InputNotify;



    async public void UpdateInput() // вынести в другой класс
    {
        while (true)
        {
            _horizontalPosition = Input.GetAxis("Horizontal");
            _verticalPosition = Input.GetAxis("Vertical");

            _rotationPositionX -= Input.GetAxis("Mouse Y");
            _rotationPositionY = Input.GetAxis("Mouse X");

            SetMoveAndRotatePosition();

            await Task.Delay(100);
            Debug.Log("Process active");
        }
    }

    private void SetMoveAndRotatePosition()
    {
        if ((_horizontalPosition >= 0.2f) || (_horizontalPosition <= 0.2) || (_verticalPosition >= 0.2f) ||
            (_verticalPosition <= 0.2f))
        {
            _positionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
            _positionToRotate = new Vector3(_rotationPositionX, _rotationPositionY, 0);
            InputNotify?.Invoke(_positionToMove, _positionToRotate);
        }
        else
        {
            _positionToMove = new Vector3(0, 0, 0);
            _positionToRotate = new Vector3(_rotationPositionX, _rotationPositionY, 0);
            InputNotify?.Invoke(_positionToMove, _positionToRotate);
        }
    }


}
