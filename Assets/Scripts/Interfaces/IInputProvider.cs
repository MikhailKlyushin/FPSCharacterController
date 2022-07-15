using UniRx;
using UnityEngine;

public interface IInputProvider
{
    public ReactiveProperty<Vector3> MovePosition { get; }
    public ReactiveProperty<Vector3> RotatePosition { get; }
}