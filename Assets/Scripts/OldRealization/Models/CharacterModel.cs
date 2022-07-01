using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    private CharacterConfig _characterConfig;

    private Guid _characterID = Guid.NewGuid();

    private Rigidbody _rigidbody;

    private InputKeyProvider _inputKeyProvider = new InputKeyProvider();
    private Vector3 _positionToMove;

    private InputCameraProvider _inputCameraProvider = new InputCameraProvider();
    private Vector3 _positionToRotate;

    private InputProvider _inputProvider;

    public CharacterModel(InputProvider input)
    {
        _inputProvider = input;
        _inputProvider.Notify += TestEvent;
    }

    private void TestEvent(Vector3 positionToMove)
    {
        Debug.Log("Event Active!!!");
        Debug.Log(positionToMove);
    }


    //private void FixedUpdate()
    //{
    //    MoveToNormalizePosition();
    //    Debug.DrawRay(transform.position, _positionToMove * 5f, Color.yellow);
    //}

    //private void MoveToNormalizePosition()
    //{
    //    _positionToMove = _inputKeyProvider.GetMovePosition();

    //    _positionToMove = transform.TransformDirection(_positionToMove);
    //    //_positionToMove = Vector3.Normalize(_positionToMove);     // падает отзывчивость управления
    //    _rigidbody.velocity = _positionToMove * _characterConfig.MoveSpeed;
    //}
}
