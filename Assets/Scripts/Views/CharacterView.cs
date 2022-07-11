using System;
using UnityEngine;
using Zenject;


public class CharacterView : MonoBehaviour
{
    public string CharacterID => _characterID;


    private string _characterID;

    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _directionHorizontal;
    private float _directionVertical;
    
    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
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
        _animator.SetFloat(_horizontal, _directionHorizontal);
        _animator.SetFloat(_vertical, _directionVertical);
    }
}
