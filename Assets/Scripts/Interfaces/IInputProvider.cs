using UniRx;
using UnityEngine;

public interface IInputProvider
{
    public ReadOnlyReactiveProperty<Vector2> MovePosition { get; }
    public ReadOnlyReactiveProperty<Vector2> RotatePosition { get; }
}