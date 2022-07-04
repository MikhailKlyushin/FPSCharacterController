using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{

   // private readonly CharacterService _playerService = new CharacterService();
    private CharacterModel _characterModel;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _speedHorizontal;
    private float _speedVertical;

    public void SetModel(CharacterModel model)
    {
        _characterModel = model;
    }

    private void Start()
    {
        //_characterModel = _playerService.CreatePlayer();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayStrafeAnimations();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _characterModel.Velocity;
        transform.localEulerAngles = _characterModel.LocalRotateAngleY;

        _speedHorizontal = _characterModel.DirectionVector.x;
        _speedVertical = _characterModel.DirectionVector.z;


        _animator.speed = 6f;
        Debug.DrawRay(transform.position, _characterModel.Velocity, Color.green);
    }

    private float GetCurrenSpeed(float speedHorizontal, float speedVertical)
    {
        speedHorizontal = Mathf.Abs(speedHorizontal);
        speedVertical = Mathf.Abs(speedVertical);

        return Mathf.Max(speedVertical, speedHorizontal);
    }

    private void PlayStrafeAnimations()
    {
        _animator.SetFloat("Horizontal", _speedHorizontal);
        _animator.SetFloat("Vertical", _speedVertical);
    }
}
