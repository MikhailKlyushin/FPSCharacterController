using System;
using UnityEngine;
using Zenject;

public class CharacterModel : IIdentified
{
    public string ID => _characterID;
    public float MoveSpeed => _config.MoveSpeed;
    public Vector3 InputVector => _inputVector;
    public Vector3 LocalRotateAngleY => _rotationVectorY;
    public Vector3 Velocity => _velocity;
    

    private readonly string _characterID = Guid.NewGuid().ToString();
    private CharacterConfig _config;
    private Vector3 _inputVector;
    private Vector3 _rotationVectorY;
    private Vector3 _velocity;
    private float _rotationPositionY;
    
    public CharacterModel(SignalBus signalBus, CharacterConfig config)
    {
        SetConfigParams(config);
        ConnectInputController(signalBus);
    }
    
    private void SetConfigParams(CharacterConfig config)
    {
        _config = config;
    }
    
    private void ConnectInputController(SignalBus signalBus)
    {
        signalBus.Subscribe<ISignalInput>(input => ChangeCharacterPosition(input.PositionToMove, input.PositionToRotate));
    }

    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _inputVector = positionToMove;

        MoveToPosition(positionToMove);
        RotateToPosition(positionToRotate);
    }

    private void MoveToPosition(Vector3 positionToMove)
    {
        _velocity = positionToMove;
        _velocity *= _config.MoveSpeed;
    }

    private void RotateToPosition(Vector3 positionToRotate)
    {
        var delta = positionToRotate.y * _config.SensivityHorizontal;
        _rotationPositionY += delta;

        _rotationVectorY = new Vector3(0, _rotationPositionY, 0);
    }
}