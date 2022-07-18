using UniRx;
using UnityEngine;

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
    
    //TODO: You dont dispose streams!!! Use .AddTo(this) - this dispose the stream after destroy GO
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    public void SetModel(CharacterModel model)
    {
        _model = model;
        _characterID = model.ID;
    }

    private void Start()
    {
        //TODO: if you want to get components correctly, add [RequireComponent] attribute under class
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _model.InputVector.Subscribe(inputVector =>
        {
            _directionHorizontal = inputVector.x;
            _directionVertical = inputVector.z;
        }).AddTo(_disposable);
        
        _model.Velocity.Subscribe(velocity =>
        {
            _velocity = velocity;
        }).AddTo(_disposable);


        _model.RotateY.Subscribe(rotate =>
        {
            _rotate = rotate;
        }).AddTo(_disposable);
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            _rigidbody.velocity = transform.TransformDirection(_velocity);
            Debug.DrawRay(transform.position, _velocity, Color.green);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotate, 0.4f);
            
            SetAnimatorParams();
        }).AddTo(_disposable);
    }

    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
        _animator.speed = _model.MoveSpeed;
    }
}
