using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [SerializeField] private CharacterConfig _characterConfig;

    private Rigidbody _rigidbody;
    private InputProvider _inputProvider = new InputProvider();
    private Vector3 _positionToMove;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToNormalizePosition();
    }

    private void MoveToNormalizePosition()
    {
        _positionToMove = _inputProvider.GetMovePosition();

        _positionToMove = transform.TransformDirection(_positionToMove);
        //_positionToMove = Vector3.Normalize(_positionToMove);     // падает отзывчивость управления
        _rigidbody.velocity = _positionToMove * _characterConfig.MoveSpeed;
    }
}
