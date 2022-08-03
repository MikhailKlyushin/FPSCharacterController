using UniRx;
using UnityEngine;


public class CharacterNetworkView : BaseCharacterNetworkView
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
        
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (IsOwner)
            {
                Position.Value = transform.position;
                Debug.Log(Position.Value);

                Rotation.Value = _rotateClient;

                Velocity.Value = _velocityClient;

                DirectionHorizontal.Value = _directionHorizontalClient;
                DirectionVertical.Value = _directionVerticalClient;

                SetCharacterMove(Rotation.Value ,Velocity.Value);
                SetAnimatorParams(DirectionHorizontal.Value, DirectionVertical.Value, 3f);
                SyncPosition(Position.Value);
            }
        }).AddTo(_disposables);
    }
}
