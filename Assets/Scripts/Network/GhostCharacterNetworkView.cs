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

        // synchronization
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            SetSyncCharacterMove(view.Velocity.Value, view.Rotation.Value);
            SetSyncAnimatorParams(view.DirectionHorizontal.Value, view.DirectionVertical.Value, 3f);
            SetSyncCharacterPosition(view.Position.Value);
        }).AddTo(_disposables);
    }
}