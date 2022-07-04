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
    public Vector3 LocalRotateAngleY => _character.transform.localEulerAngles;


    private CharacterConfig _characterConfig;
    private GameObject _character = new GameObject();

    private IInputProvider _inputProvider;

    private Vector3 _positionToMove;
    private Vector3 _positionToRotate;

    private Vector3 _rotationVectorX;
    private Vector3 _rotationVectorY;

    private float _rotationPositionX;
    private float _rotationPositionY;
    private float _delta;

    public float sensivityHorisontal = 20f;
    public float sensivityVertical = 20f;

    public float minimumVertical = -45f;
    public float maximumVertical = 45f;

    public Vector3 DirectionVector;

    private float _moveSpeed = 3;
    public float MoveSpeed => _moveSpeed;


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


    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _positionToMove = positionToMove;
        DirectionVector = positionToMove;

        _positionToRotate = positionToRotate;

        MoveToPosition();
        RotateToPosition();

        //Debug.Log("rotate: " + _positionToRotate);
        //Debug.Log("velocity: " + _velocity);
    }

    private void MoveToPosition()
    {
        _positionToMove = _character.transform.TransformDirection(_positionToMove);
        //_positionToMove = Vector3.Normalize(_positionToMove);     // появляется инпут лаг
        _velocity = _positionToMove * _moveSpeed;
        //_velocity = NormalizeVelocity(_velocity);
    }

    private Vector3 NormalizeVelocity(Vector3 velocity)
    {
        velocity.x = LimitSpeed(velocity.x);
        //velocity.y = LimitSpeed(velocity.y);
        velocity.z = LimitSpeed(velocity.z);
        return velocity;
    }

    private float LimitSpeed(float speed)
    {
        if (speed > _moveSpeed)
        {
            return _moveSpeed;
        }
        else if (speed < -_moveSpeed)
        {
            return -_moveSpeed;
        }
        else
        {
            return speed;
        }
    }
    
    private void RotateToPosition()
    {
        _rotationPositionX -= _positionToRotate.x * sensivityVertical;
        _rotationPositionX = Mathf.Clamp(_rotationPositionX, minimumVertical, maximumVertical);

        _delta = _positionToRotate.y * sensivityHorisontal;
        _rotationPositionY = _character.transform.localEulerAngles.y + _delta;

        _rotationVectorX = new Vector3(_rotationPositionX, 0, 0);
        _rotationVectorY = new Vector3(0, _rotationPositionY, 0);

        _character.transform.localEulerAngles = _rotationVectorY;
    }
}
