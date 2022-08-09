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

        var state = GetComponent<CharacterState>();

        // synchronization
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            SetSyncCharacterMove(state.Velocity.Value, state.Rotation.Value);
            SetSyncAnimatorParams(state.DirectionHorizontal.Value, state.DirectionVertical.Value, 3f);
            SetSyncCharacterPosition(state.Position.Value);
        }).AddTo(_disposables);
    }
}