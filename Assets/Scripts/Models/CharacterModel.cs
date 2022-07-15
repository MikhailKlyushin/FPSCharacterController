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
            InputVector.SetValueAndForceNotify(positionToMove);
            MoveToPosition(positionToMove);
        }).AddTo(_disposable);

        _input.RotatePosition.Subscribe(positionToRotate =>
        {
            RotateToPosition(positionToRotate);
        }).AddTo(_disposable);
    }

    /*private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        InputVector.SetValueAndForceNotify(positionToMove);
        
        MoveToPosition(positionToMove);
        RotateToPosition(positionToRotate);
    }*/

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
        
        //TODO: rewrite input
        RotateY.Value = Quaternion.Lerp(RotateY.Value, _rotateY, 0.8f);
    }
}