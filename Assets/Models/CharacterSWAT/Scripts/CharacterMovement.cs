using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    public float Speed;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _position;

    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_VETRICAL = "Vertical";

    private const float _distanceOffsetCamera = 5f;
    private Vector3 _targetRotate => _camera.forward * _distanceOffsetCamera;

    private Vector3 _direction;
    private Quaternion _look;
    private const float _angularSpeedCharacter = 400f;

    private float _horizontal;
    private float _vertical;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPosition();
        Rotate();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _position * Speed;
       // PlayAnimation();
    }

    private void MoveToPosition()
    {
        _horizontal = Input.GetAxis(STR_HORIZONTAL);
        _vertical = Input.GetAxis(STR_VETRICAL);
        _position = new Vector3(_horizontal, 0, _vertical);
        _position = transform.TransformDirection(_position);
        Vector3.Normalize(_position);
    }

    private void Rotate()
    {
        Vector3 tartget = _targetRotate;
        tartget.y = 0;

        _look = Quaternion.LookRotation(tartget);
        float speed = _angularSpeedCharacter * Time.deltaTime; // скорость указать
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed);
    }

    private void PlayAnimation()
    {
        _animator.SetFloat(STR_HORIZONTAL, _horizontal);
        Debug.Log(_horizontal);
        _animator.SetFloat(STR_VETRICAL, _vertical);
    }
}
