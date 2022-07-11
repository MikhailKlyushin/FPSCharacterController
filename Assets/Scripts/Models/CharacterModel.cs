using System;
using UnityEngine;

public class CharacterModel
{
    #region Public params
    public string CharacterID => _characterID;
    public float MoveSpeed => _config.MoveSpeed;
    public Vector3 InputVector => _inputVector;
    public Vector3 LocalRotateAngleY => _rotationVectorY;
    public Vector3 Velocity => _velocity;

    #endregion

    private readonly string _characterID = Guid.NewGuid().ToString();
    private CharacterConfig _config;
    private Vector3 _inputVector;
    private Vector3 _rotationVectorY;
    
    public CharacterModel(IInputProvider inputController, CharacterConfig config)
    {
        SetConfigParams(config);
        ConnectInputController(inputController);
    }
    
    private void SetConfigParams(CharacterConfig config)
    {
        _config = config;
    }
    
    private void ConnectInputController(IInputProvider input)
    {
        input.InputNotify += ChangeCharacterPosition;
    }

    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _inputVector = positionToMove;

        MoveToPosition(positionToMove);
        RotateToPosition(positionToRotate);
    }
    
    private Vector3 _velocity;

    private void MoveToPosition(Vector3 positionToMove)
    {
        _velocity = positionToMove;
        _velocity *= _config.MoveSpeed;
    }

    //TODO: He is so alone(((
    private float _rotationPositionY;

    private void RotateToPosition(Vector3 positionToRotate)
    {
        var delta = positionToRotate.y * _config.SensivityHorizontal;
        _rotationPositionY += delta;

        _rotationVectorY = new Vector3(0, _rotationPositionY, 0);
    }
}