using UniRx;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]

public class CharacterView : NetworkBehaviour, IIdentified
{
    public string ID => _characterID;
    private string _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _velocityModel;
    private Quaternion _rotateModel;

    private NetworkVariable<Vector3> _velocity = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> _rotate = new NetworkVariable<Quaternion>();
    
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
        _characterID = model.ID;
        
        _model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontal = inputVector.x;
            _directionVertical = inputVector.z;
        }).AddTo(_disposables);
        
        _model.Velocity.Subscribe(velocity => { _velocityModel = velocity; }).AddTo(_disposables);
        _model.RotateY.Subscribe(rotate => { _rotateModel = rotate; }).AddTo(_disposables);

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (IsClient && IsOwner)
            {
                UpdateClientPositionAndRotationServerRpc(_velocityModel, _rotateModel);
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
    }

    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
        _animator.speed = 3f; //_model.MoveSpeed;
    }
    
    [ServerRpc]
    public void UpdateClientPositionAndRotationServerRpc(Vector3 newPosition, Quaternion newRotate)
    {
        _velocity.Value = newPosition;
        _rotate.Value = newRotate;
    }
}
