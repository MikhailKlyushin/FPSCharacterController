using System;
using UnityEngine;


public class CharacterView : MonoBehaviour
{
    public Guid CharacterID => _characterID;


    private Guid _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _directionHorizontal;
    private float _directionVertical;

    public void SetModel(CharacterModel model)
    {
        _characterID = model.CharacterID;
        _model = model;
    }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

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
        _rigidbody.velocity = transform.TransformDirection(_model.Velocity);
        transform.localEulerAngles = _model.LocalRotateAngleY;

        _directionHorizontal = _model.InputVector.x;
        _directionVertical = _model.InputVector.z;

        _animator.speed = _model.MoveSpeed;

        Debug.DrawRay(transform.position, _model.Velocity, Color.green);
    }

    private static readonly float _horizontal;
    private static readonly float _vertical;

    private void RunStrafeAnimations()
    {
        //TODO: cache this: private static readonly int _horizontal = Animator.StringToHash("Horizontal"); by example
        _animator.SetFloat("Horizontal", _directionHorizontal);
        _animator.SetFloat("Vertical", _directionVertical);
        
        
        _animator.GetFloat("Horivontal");
        _animator.GetFloat("Vertical");
    }
}
