using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMovement : MonoBehaviour
{ 
    public float Speed;
    //public Transform _cameraTPS;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _position;

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
        _position = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
