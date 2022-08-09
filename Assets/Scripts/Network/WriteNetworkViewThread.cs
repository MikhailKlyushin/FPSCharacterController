using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class WriteNetworkViewThread : BaseCharacterNetworkView, INetworkViewThread
{
    public void StartThread()
    {
        /*Observable.EveryFixedUpdate().Subscribe(_ =>
        {

            Position.Value = transform.position;

            Rotation.Value = _rotateClient;
            Velocity.Value = _velocityClient;

            SetSyncCharacterMove(Velocity.Value, Rotation.Value);

            DirectionHorizontal.Value = _directionHorizontalClient;
            DirectionVertical.Value = _directionVerticalClient;

            SetSyncAnimatorParams(DirectionHorizontal.Value, DirectionVertical.Value, 3f);

        }).AddTo(_disposables);*/
    }
}
