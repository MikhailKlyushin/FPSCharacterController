using UniRx;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class NetworkView : NetworkBehaviour
{
    //public string ID => _characterID;
    //private string _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    private NetworkVariable<Vector3> _networkVelocity = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> _networkRotateY = new NetworkVariable<Quaternion>();
    
    private float _directionHorizontal;
    private float _directionVertical;
    
    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public void SetModel(CharacterModel model)
    {
        _model = model;
        SubscribeEveryFixedUpdate();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        SubscribeEveryFixedUpdate();
    }

    private void SubscribeInputVector()
    {
        _model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontal = inputVector.x;
            _directionVertical = inputVector.z;
            
        }).AddTo(_disposables);
    }
    

    private void SubscribeEveryFixedUpdate()
    {
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            _rigidbody.velocity = _networkVelocity.Value;

            transform.rotation = Quaternion.Lerp(transform.rotation, _networkRotateY.Value, 0.4f);

            SetAnimatorParams();
            
        }).AddTo(_disposables);
    }
    
    [ServerRpc]
    public void UpdateClientPositionAndRotationOnServerRpc(Vector3 newPosition, Quaternion newRotation)
    {
        _networkVelocity.Value = newPosition;
        _networkRotateY.Value = newRotation;
    }
    
    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
        _animator.speed = _model.MoveSpeed;
    }
}
