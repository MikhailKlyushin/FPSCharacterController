using System;
using UnityEngine;

public class CharacterModel
{
    #region Public params

    public Guid CharacterID => _characterID;

    public float MoveSpeed => _moveSpeed;

    public Vector3 Velocity => _velocity;

    public Vector3 LocalRotate => _localRotate;
    public Vector3 LocalRotateAngleX => _rotationVectorX;
    public Vector3 LocalRotateAngleY => _rotationVectorY;
    public Vector3 InputVector => _inputVector;

    #endregion

    #region Configuration params

    private float _moveSpeed;

    private float _sensivityHorisontal;
    private float _sensivityVertical;

    private float _minimumVerticalAngle;
    private float _maximumVerticalAngle;

    #endregion

    private Guid _characterID = Guid.NewGuid();

    private IInputProvider _input;

    private Vector3 _localRotate;
    private Vector3 _rotationVectorX;
    private Vector3 _rotationVectorY;


    public CharacterModel(IInputProvider inputController, CharacterConfig config)
    {
        SetConfigParams(config);
        ConnectInputController(inputController);
    }
    
    private void SetConfigParams(CharacterConfig config)
    {
        _moveSpeed = config.MoveSpeed;
        _sensivityHorisontal = config.SensivityHorizontal;
        _sensivityVertical = config.SensivityVertical;
        _minimumVerticalAngle = config.MinimumVerticalAngle;
        _maximumVerticalAngle = config.MaximumVerticalAngle;
    }
    
    private void ConnectInputController(IInputProvider input)
    {
        input.InputNotify += ChangeCharacterPosition;
    }


    private Vector3 _inputVector;
    private Vector3 _positionToMove;
    private Vector3 _positionToRotate;

    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _inputVector = positionToMove;
        _positionToMove = positionToMove;
        _positionToRotate = positionToRotate;

        MoveToPosition();
        RotateToPosition();
    }
    
    private Vector3 _velocity;

    private void MoveToPosition()
    {
        _velocity = _positionToMove;
        _velocity.x += _positionToRotate.x;
        _velocity *= _moveSpeed;
    }

    private float _rotationPositionX;
    private float _rotationPositionY;

    private void RotateToPosition()
    {
        _rotationPositionX -= _positionToRotate.x * _sensivityVertical;
        _rotationPositionX = Mathf.Clamp(_rotationPositionX, _minimumVerticalAngle, _maximumVerticalAngle);

        var delta = _positionToRotate.y * _sensivityHorisontal;
        _rotationPositionY += delta;

        _rotationVectorX = new Vector3(_rotationPositionX, 0, 0);

        _rotationVectorY = new Vector3(0, _rotationPositionY, 0);

        _localRotate = new Vector3(_rotationPositionX, _rotationPositionY, 0);
    }
}