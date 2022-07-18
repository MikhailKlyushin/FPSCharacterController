using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]

public class CharacterView : MonoBehaviour, IIdentified
{
    public string ID => _characterID;
    private string _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _velocity;
    private Quaternion _rotate;
    
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
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontal = inputVector.x;
            _directionVertical = inputVector.z;
        }).AddTo(_disposables);
        
        _model.Velocity.Subscribe(velocity =>
        {
            _velocity = velocity;
        }).AddTo(_disposables);


        _model.RotateY.Subscribe(rotate =>
        {
            _rotate = rotate;
        }).AddTo(_disposables);
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            _rigidbody.velocity = transform.TransformDirection(_velocity);
            Debug.DrawRay(transform.position, _velocity, Color.green);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotate, 0.4f);
            
            SetAnimatorParams();
        }).AddTo(_disposables);
    }

    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
        _animator.speed = _model.MoveSpeed;
    }
}
