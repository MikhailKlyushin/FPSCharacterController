using UniRx;
using UnityEngine;


[RequireComponent(typeof(CharacterNetworkView))]

public class GhostCharacterNetworkView : BaseCharacterNetworkView
{
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Destroy(this);
        }

        var view = GetComponent<CharacterNetworkView>();

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            SetCharacterMove(view.Rotation.Value, view.Velocity.Value);
            SetAnimatorParams(view.DirectionHorizontal.Value, view.DirectionVertical.Value, 3f);
        }).AddTo(_disposables);
    }
}