using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _speedHorizontal;
    private float _speedVertical;
    private float _lengthVelocityVector;


    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_VETRICAL = "Vertical";


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayStrafeAnimations();
    }

    void FixedUpdate()
    {
        //_speedHorizontal = _rigidbody.velocity.x;
        //_speedVertical = _rigidbody.velocity.z;
        _speedHorizontal = Input.GetAxis(STR_HORIZONTAL);
        _speedVertical = Input.GetAxis(STR_VETRICAL);

        _lengthVelocityVector = _rigidbody.velocity.magnitude;


        _animator.speed = CurrenSpeed(_speedHorizontal, _speedVertical);
        Debug.Log("Speeed: " + _animator.speed);
    }

    private void PlayStrafeAnimations()
    {
        _animator.SetFloat(STR_HORIZONTAL, _speedHorizontal);
        _animator.SetFloat(STR_VETRICAL, _speedVertical);
    }

    private float CurrenSpeed(float speedHorizontal, float speedVertical)
    {
        speedHorizontal = Mathf.Abs(speedHorizontal);
        speedVertical = Mathf.Abs(speedVertical);

        if (speedHorizontal >= speedVertical)
        {
            return speedHorizontal;
        }
        else
        {
            return speedVertical;
        }
    }
}
