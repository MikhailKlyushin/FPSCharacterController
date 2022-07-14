using UniRx;
using UnityEngine;

public class CharacterView : MonoBehaviour, IIdentified
{
    public string ID => _characterID;
    private string _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _directionHorizontal;
    private float _directionVertical;
    
    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

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

            SetAnimatorParams();

        }).AddTo(_disposable);
        
        _model.Velocity.Subscribe(velocity =>
        {
            _rigidbody.velocity = transform.TransformDirection(velocity);
            Debug.DrawRay(transform.position, velocity, Color.green);
            
        }).AddTo(_disposable);

        _model.RotateY.Subscribe(angle =>
        {
            //transform.rotation = angle;
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, 0.4f);

        }).AddTo(_disposable);
    }

    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
        _animator.speed = _model.MoveSpeed;
    }
}
