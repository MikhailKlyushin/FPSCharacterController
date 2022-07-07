using UnityEngine;

public interface IInputProvider
{
    public delegate void InputHandler(Vector3 movePosition, Vector3 rotatePosition);

    public event InputHandler InputNotify;
}