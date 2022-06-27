using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _speedX;
    private float _speedZ;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _speedX = _rigidbody.velocity.x;
        _speedZ = _rigidbody.velocity.z;
    }

    void FixedUpdate()
    {

    }
}
