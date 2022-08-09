using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class WriteNetworkViewThread : BaseCharacterNetworkView, INetworkViewThread
{
    private Vector3 _velocityClient;
    private Quaternion _rotateClient;
    
    private float _directionHorizontalClient;
    private float _directionVerticalClient;

    public void SetModel(ICharacterModel model)
    {
        SetID(model.ID);

        model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontalClient = inputVector.x;
            _directionVerticalClient = inputVector.z;
        }).AddTo(_disposables);

        model.Velocity.Subscribe(velocity => { _velocityClient = velocity; }).AddTo(_disposables);
        model.RotateY.Subscribe(rotate => { _rotateClient = rotate; }).AddTo(_disposables);
    }

    public void StartThread(CharacterNetworkState state)
    {
        // synchronization
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {

            state.Position.Value = transform.position;

            state.Rotation.Value = _rotateClient;
            state.Velocity.Value = _velocityClient;

            SetSyncCharacterMove(state.Velocity.Value, state.Rotation.Value);

            state.DirectionHorizontal.Value = _directionHorizontalClient;
            state.DirectionVertical.Value = _directionVerticalClient;

            SetSyncAnimatorParams(state.DirectionHorizontal.Value, state.DirectionVertical.Value, 3f);
        }).AddTo(_disposables);
    }
}
