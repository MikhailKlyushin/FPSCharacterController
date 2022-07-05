using UnityEngine;

public interface IInputProvider     // возможно переписать на абстрактный класс
{
    public delegate void InputHandler(Vector3 movePosition, Vector3 rotatePosition);

    public event InputHandler InputNotify;
    void UpdateInput();     // в объявлении добавить async
}
