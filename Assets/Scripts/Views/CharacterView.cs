using System;
using UnityEngine;
using Zenject;


public class CharacterView : MonoBehaviour
{
    public Guid CharacterID => _characterID;


    private Guid _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _directionHorizontal;
    private float _directionVertical;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    
    public void SetModel(CharacterModel model)
    {
        _model = model;
        _characterID = model.CharacterID;
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

    private void RunStrafeAnimations()
    {
        _animator.SetFloat(Horizontal, _directionHorizontal);
        _animator.SetFloat(Vertical, _directionVertical);
    }
}
