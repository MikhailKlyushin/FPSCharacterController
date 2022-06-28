using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMovement : MonoBehaviour
{ 
    public float Speed;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private Vector3 _position;

    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_VETRICAL = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        InputValue();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _position * Speed;
    }

    private void InputValue()
    {
        _position = new Vector3(Input.GetAxis(STR_HORIZONTAL), 0, Input.GetAxis(STR_VETRICAL));
    }
}
