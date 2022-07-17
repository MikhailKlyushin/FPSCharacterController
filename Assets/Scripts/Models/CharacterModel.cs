using System;
using UniRx;
using UnityEngine;
using Zenject;

public class CharacterModel : IIdentified
{
    public string ID => _characterID;
    public float MoveSpeed => _config.MoveSpeed;
    public ReactiveProperty<Vector3> InputVector { get; } = new ReactiveProperty<Vector3>();
    public ReactiveProperty<Vector3> Velocity { get; } = new ReactiveProperty<Vector3>();
    public ReactiveProperty<Quaternion> RotateY { get; } = new ReactiveProperty<Quaternion>();

    private readonly string _characterID = Guid.NewGuid().ToString();
    private readonly CompositeDisposable _disposable = new CompositeDisposable();
    private readonly CharacterConfig _config;
    private readonly IInputProvider _input;
    
    private Vector3 _rotationVectorY;
    private Vector3 _velocity;
    private float _rotationPositionY;
    private Quaternion _rotateY;
    

    public CharacterModel(IInputProvider input, CharacterConfig config)
    {
        _config = config;
        _input = input;

        ConnectInputNotification();
    }

    private void ConnectInputNotification()
    {
        _input.MovePosition.Subscribe(positionToMove =>
        {
            var positionToMoveVector3 = new Vector3(positionToMove.x, 0, positionToMove.y);
            InputVector.SetValueAndForceNotify(positionToMoveVector3);
            
            MoveToPosition(positionToMoveVector3);
        }).AddTo(_disposable);

        _input.RotatePosition.Subscribe(positionToRotate =>
        {
            var positionToRotateVector3 = new Vector3(positionToRotate.y, positionToRotate.x, 0);
            RotateToPosition(positionToRotateVector3);
        }).AddTo(_disposable);
    }

    private void MoveToPosition(Vector3 positionToMove)
    {
        _velocity = positionToMove;
        _velocity *= _config.MoveSpeed;
        Velocity.SetValueAndForceNotify(_velocity);
    }

    private void RotateToPosition(Vector3 positionToRotate)
    {
        var delta = positionToRotate.y * _config.SensitivityHorizontal * Time.deltaTime;
        _rotationPositionY += delta;

        _rotationVectorY.y = _rotationPositionY;
        _rotateY = Quaternion.Euler(_rotationVectorY);
        
        RotateY.Value = Quaternion.Lerp(RotateY.Value, _rotateY, _config.SmoothRotate);
    }
}