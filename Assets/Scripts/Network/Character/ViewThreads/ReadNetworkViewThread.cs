using UniRx;
using UnityEngine;

public class ReadNetworkViewThread : BaseCharacterNetworkView, INetworkViewThread
{
    public void SetModel(ICharacterModel model)
    {
        Debug.Log("Model this not used!");
    }

    public void StartThread(CharacterNetworkState state)
    {
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            SetSyncCharacterMove(state.Velocity.Value, state.Rotation.Value);
            SetSyncAnimatorParams(state.DirectionHorizontal.Value, state.DirectionVertical.Value, 3f);
            SetSyncCharacterPosition(state.Position.Value);
        }).AddTo(_disposables);
    }
}
