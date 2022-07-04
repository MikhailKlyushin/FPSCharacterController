using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    private Guid _characterID = Guid.NewGuid();
    public Guid CharacterID => _characterID;

    private Vector3 _velocity;
    public Vector3 Velocity => _velocity;


    private CharacterConfig _characterConfig;
    private GameObject _character = new GameObject();

    private IInputProvider _inputProvider;

    private Vector3 _positionToMove;
    private Vector3 _positionToRotate;

    public CharacterModel()
    {
        // пустой конструктор
    }

    public CharacterModel(IInputProvider input)
    {
        //SetStartPosition();

        _inputProvider = input;
        _inputProvider.InputNotify += ChangeCharacterPosition;
        _inputProvider.UpdateInput();   // запуск асинхронного метода

        _characterConfig = new CharacterConfig();
    }

    private void SetStartPosition()
    {
        _character.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        Debug.Log("Event Active!!!");
        _positionToMove = positionToMove;
        MoveToNormalizePosition();
        //RotateToTarget();


        Debug.Log("velocity: " + _velocity);
    }

    private void MoveToNormalizePosition()
    {
        _positionToMove = _character.transform.TransformDirection(_positionToMove);
        //_positionToMove = Vector3.Normalize(_positionToMove);
        _velocity = _positionToMove * 5; //_characterConfig.MoveSpeed;
    }

    // доделать поворот

    //private void RotateToTarget()
    //{
    //    Vector3 tartget = _positionToRotate.forvatd;
    //    tartget.y = 0;
        
    //    _rotationAngle = Quaternion.LookRotation(tartget);
    //    float speedRotation = _angularSpeedCharacter * Time.deltaTime;
    //    _character.transform.rotation = Quaternion.RotateTowards(_charactertransform.rotation, _rotationAngle, speedRotation);
    //}
}
