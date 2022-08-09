using UniRx;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class CharacterNetworkView : BaseCharacterNetworkView
{
    private Vector3 _velocityClient;
    private Quaternion _rotateClient;
    
    private float _directionHorizontalClient;
    private float _directionVerticalClient;

    private CharacterState _state;
    
    public void SetModel(ICharacterModel model)
    {
        SetID(model.ID);

        _state = GetComponent<CharacterState>();

        model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontalClient = inputVector.x;
            _directionVerticalClient = inputVector.z;
        }).AddTo(_disposables);

        model.Velocity.Subscribe(velocity => { _velocityClient = velocity; }).AddTo(_disposables);
        model.RotateY.Subscribe(rotate => { _rotateClient = rotate; }).AddTo(_disposables);
        
        // synchronization
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (IsOwner)
            {
                _state.Position.Value = transform.position;
                
                _state.Rotation.Value = _rotateClient;
                _state.Velocity.Value = _velocityClient;
                
                SetSyncCharacterMove(_state.Velocity.Value, _state.Rotation.Value);

                _state.DirectionHorizontal.Value = _directionHorizontalClient;
                _state.DirectionVertical.Value = _directionVerticalClient;
                
                SetSyncAnimatorParams(_state.DirectionHorizontal.Value, _state.DirectionVertical.Value, 3f);
            }
        }).AddTo(_disposables);
    }
}
