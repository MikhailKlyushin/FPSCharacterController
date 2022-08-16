using UniRx;
using UnityEngine;

public class ReadNetworkViewThread : BaseSyncCharacter, INetworkViewThread
{
    public void SetModel(ICharacterModel model)
    {
        Debug.Log("Model this not used!");
    }

    public void StartThread(Transform transform, Rigidbody rigidbody, Animator animator, CharacterNetworkParams state)
    {
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            SetSyncCharacterMove(transform, rigidbody, state.Velocity.Value, state.Rotation.Value);
            SetSyncAnimatorParams(animator, state.DirectionHorizontal.Value, state.DirectionVertical.Value, 3f);
            SetSyncCharacterPosition(transform, state.Position.Value);
        }).AddTo(transform);

    }
}
