using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{

    private readonly CharacterService _playerService = new CharacterService();
    private CharacterModel _characterModel;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _speedHorizontal;
    private float _speedVertical;

    private void Start()
    {
        _characterModel = _playerService.CreatePlayer();
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

        //Vector3 vector3 = Vector3.Normalize(_characterModel.Velocity);

        _speedHorizontal = Input.GetAxis("Horizontal");
        _speedVertical = Input.GetAxis("Vertical");

        _animator.speed = GetCurrenSpeed(_characterModel.DirectionVector.x, _characterModel.DirectionVector.z);

        /////////// допилить
        Debug.DrawRay(transform.position, _characterModel.Velocity, Color.green);

       // Debug.Log("SPEED = " + _characterModel.DirectionVector.x);
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
