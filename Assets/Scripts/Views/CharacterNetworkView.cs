using UniRx;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Animator))]

public class CharacterNetworkView : NetworkBehaviour, IIdentified
{
    public string ID => _characterID;
    private string _characterID;

    private ICharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    private Vector3 _positionClient;
    private Vector3 _rotationClient;

    private Vector3 _velocityClient;
    private Quaternion _rotateClient;
    
    private float _directionHorizontalClient;
    private float _directionVerticalClient;
    
    private NetworkVariable<Vector3> _position = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<Quaternion> _rotation = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<Vector3> _velocity = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<Quaternion> _rotate = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> _directionHorizontal = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> _directionVertical = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    

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
                _velocity.Value = _velocityClient;
                _rotate.Value = _rotateClient;
                _directionHorizontal.Value = _directionHorizontalClient;
                _directionVertical.Value = _directionVerticalClient;
            }

            SetCharacterMove(_velocity.Value, _rotate.Value);
            SetAnimatorParams(_directionHorizontal.Value, _directionVertical.Value, 3f);

        }).AddTo(_disposables);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        var netAnimator = GetComponent<NetworkAnimator>();  //TODO: rewrite this
        netAnimator.Animator.speed = 3f;
    }

    private void SetCharacterMove(Vector3 velocity, Quaternion rotate)
    {
        _rigidbody.velocity = transform.TransformDirection(velocity);
        Debug.DrawRay(transform.position, velocity, Color.green);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 0.4f);
    }

    private void SetAnimatorParams(float directionHorizontal, float directionVertical, float speed)
    {
        _animator.SetFloat(_horizontal, directionHorizontal);
        _animator.SetFloat(_vertical, directionVertical);
        _animator.speed = speed;
    }
}
