using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public Guid CharacterID => _characterID;
    public Vector3 Velocity => _velocity;

    private Guid _characterID = Guid.NewGuid();

    private CharacterConfig _characterConfig;

    private GameObject _character = new GameObject();

    //private Transform _transform;
    private Vector3 _velocity;

    private InputProvider _inputProvider;

    private Vector3 _positionToMove;
    private Vector3 _positionToRotate;

    public CharacterModel(InputProvider input)
    {
        SetStartPosition();

        _inputProvider = input;
        _inputProvider.Notify += MoveCharacter;
        _inputProvider.Update();

        _characterConfig = new CharacterConfig();
    }

    private void SetStartPosition()
    {
        _character.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void MoveCharacter(Vector3 positionToMove)
    {
        Debug.Log("Event Active!!!");
        _positionToMove = positionToMove;
        MoveToNormalizePosition();


        Debug.Log("velocity: " + _velocity);
    }

    private void MoveToNormalizePosition()
    {
        _positionToMove = _character.transform.TransformDirection(_positionToMove);
        //_positionToMove = Vector3.Normalize(_positionToMove);
        _velocity = _positionToMove * 5; //_characterConfig.MoveSpeed;
    }
}
