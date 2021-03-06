using UniRx;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]

//TODO: make a separate network realization

public class CharacterView : NetworkBehaviour, IIdentified
{
    public string ID => _characterID;
    private string _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _velocityClient;
    private Quaternion _rotateClient;
    
    private float _directionHorizontalClient;
    private float _directionVerticalClient;

    private NetworkVariable<Vector3> _velocity = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> _rotate = new NetworkVariable<Quaternion>();
    private NetworkVariable<float> _directionHorizontal = new NetworkVariable<float>();
    private NetworkVariable<float> _directionVertical = new NetworkVariable<float>();
    

    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public override void OnDestroy()
    {
        _disposables.Dispose();
    }

    public void SetModel(CharacterModel model)
    {
        _model = model;
        _characterID = model.ID;
        
        _model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontalClient = inputVector.x;
            _directionVerticalClient = inputVector.z;
        }).AddTo(_disposables);
        
        _model.Velocity.Subscribe(velocity => { _velocityClient = velocity; }).AddTo(_disposables);
        _model.RotateY.Subscribe(rotate => { _rotateClient = rotate; }).AddTo(_disposables);

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (IsOwner)
            {
                UpdateClientPositionAndRotationServerRpc(_velocityClient, _rotateClient);
                UpdateClientAnimatorServerRpc(_directionHorizontalClient, _directionVerticalClient);
            }
            
            _rigidbody.velocity = transform.TransformDirection(_velocity.Value);
            Debug.DrawRay(transform.position, _velocity.Value, Color.green);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotate.Value, 0.4f);

            SetAnimatorParams();
        }).AddTo(_disposables);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        var netAnimator = GetComponent<NetworkAnimator>();  //TODO: rewrite this
        netAnimator.Animator.speed = 3f;
    }

    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal.Value);
        _animator.SetFloat(_vertical, _directionVertical.Value);
        _animator.speed = 3f; //_model.MoveSpeed;   TODO: rewrite via config
    }
    
    [ServerRpc]
    private void UpdateClientPositionAndRotationServerRpc(Vector3 newPosition, Quaternion newRotate)
    {
        _velocity.Value = newPosition;
        _rotate.Value = newRotate;
    }

    [ServerRpc]
    private void UpdateClientAnimatorServerRpc(float newHorizontal, float newVertical)
    {
        _directionHorizontal.Value = newHorizontal;
        _directionVertical.Value = newVertical;
    }
}
