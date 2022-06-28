using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _speedHorizontal;
    private float _speedVertical;

    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_VETRICAL = "Vertical";


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _speedHorizontal= _rigidbody.velocity.x;
        _speedVertical = _rigidbody.velocity.z;
        PlayStrafeAnimations();
    }

    private void PlayStrafeAnimations()
    {
        _animator.SetFloat(STR_HORIZONTAL, _speedHorizontal);
        _animator.SetFloat(STR_VETRICAL, _speedVertical);
        _animator.SetFloat("Speed", 1);
    }
}
