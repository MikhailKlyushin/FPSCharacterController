using UnityEngine;

public interface IInputProvider
{
    //TODO: Search SignalBus and implement it here
    public delegate void InputHandler(Vector3 movePosition, Vector3 rotatePosition);

    public event InputHandler InputNotify;
}