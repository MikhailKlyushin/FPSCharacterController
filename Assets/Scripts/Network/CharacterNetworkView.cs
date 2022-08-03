using UniRx;
using Unity.Netcode;
using UnityEngine;


public class CharacterNetworkView : BaseCharacterNetworkView
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    public NetworkVariable<Vector3> Velocity = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public NetworkVariable<float> DirectionHorizontal = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> DirectionVertical = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
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
                
                Rotation.Value = _rotateClient;

                Velocity.Value = _velocityClient;

                DirectionHorizontal.Value = _directionHorizontalClient;
                DirectionVertical.Value = _directionVerticalClient;

                SetCharacterMove( Velocity.Value,Position.Value,Rotation.Value);
                SetAnimatorParams(DirectionHorizontal.Value, DirectionVertical.Value, 3f);
            }
        }).AddTo(_disposables);
    }
}
