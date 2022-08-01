using UniRx;
using UnityEngine;

public interface ICharacterModel
{
    public string ID { get; }
    public ReadOnlyReactiveProperty<Vector3> InputVector { get; }
    public ReadOnlyReactiveProperty<Vector3> Velocity { get; }
    public ReadOnlyReactiveProperty<Quaternion> RotateY { get; }
}
