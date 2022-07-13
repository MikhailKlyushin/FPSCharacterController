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

    private CompositeDisposable _disposable = new CompositeDisposable();
    
    public void SetModel(CharacterModel model)
    {
        _model = model;
        _characterID = model.ID;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        _model.Velocity.Subscribe(velocity =>
        {
            _rigidbody.velocity = transform.TransformDirection(velocity);
            Debug.Log("Rigidbody.Vel = " + _rigidbody.velocity);
            Debug.DrawRay(transform.position, velocity, Color.green);
        }).AddTo(_disposable);
        /*_model.RotateY.Subscribe(angle =>
        {
            transform.rotation = angle; //Quaternion.Lerp( transform.rotation,angle, 0.1f);
        }).AddTo(_disposable);*/
    }

    private void Update()
    {
        RunStrafeAnimations();
    }

    private void FixedUpdate()
    {
        UpdateViewModel();
    }

    private void UpdateViewModel()
    {
        transform.rotation = _model.RotateY;

        _directionHorizontal = _model.InputVector.x;
        _directionVertical = _model.InputVector.z;

        _animator.speed = _model.MoveSpeed;
    }

    private void RunStrafeAnimations()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
    }
}
