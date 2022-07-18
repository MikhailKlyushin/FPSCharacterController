using System;
using UniRx;
using UnityEngine;
using Zenject;

public class CharacterModel : IIdentified
{
    public string ID => _characterID;
    public float MoveSpeed => _config.MoveSpeed;
    public ReadOnlyReactiveProperty<Vector3> InputVector => _inputVector.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<Vector3> Velocity => _velocity.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<Quaternion> RotateY => _rotateY.ToReadOnlyReactiveProperty();


    private ReactiveProperty<Vector3> _inputVector = new ReactiveProperty<Vector3>();
    private ReactiveProperty<Vector3> _velocity = new ReactiveProperty<Vector3>();
    private ReactiveProperty<Quaternion> _rotateY = new ReactiveProperty<Quaternion>();

    private readonly string _characterID = Guid.NewGuid().ToString();
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    private readonly CharacterConfig _config;
    private readonly IInputProvider _input;
    
    private Vector3 _rotationVectorY;
    private float _rotationPositionY;


    public CharacterModel(IInputProvider input, CharacterConfig config)
    {
        _config = config;
        _input = input;

        ConnectInputNotification();
    }

    ~CharacterModel()
    {
        _disposables.Dispose();
    }

    private void ConnectInputNotification()
    {
        _input.MovePosition.Subscribe(positionToMove =>
        {
            var positionToMoveVector3 = new Vector3(positionToMove.x, 0, positionToMove.y);
            _inputVector.SetValueAndForceNotify(positionToMoveVector3);
            
            MoveToPosition(positionToMoveVector3);
        }).AddTo(_disposables);

        _input.RotatePosition.Subscribe(positionToRotate =>
        {
            var positionToRotateVector3 = new Vector3(positionToRotate.y, positionToRotate.x, 0);
            RotateToPosition(positionToRotateVector3);
        }).AddTo(_disposables);
    }

    private void MoveToPosition(Vector3 positionToMove)
    {
        positionToMove *= _config.MoveSpeed;
        _velocity.SetValueAndForceNotify(positionToMove);
    }

    private void RotateToPosition(Vector3 positionToRotate)
    {
        var delta = positionToRotate.y * _config.SensitivityHorizontal * Time.deltaTime;
        _rotationPositionY += delta;

        _rotationVectorY.y = _rotationPositionY;
        var rotateY = Quaternion.Euler(_rotationVectorY);
        
        _rotateY.SetValueAndForceNotify(Quaternion.Lerp(RotateY.Value, rotateY, _config.SmoothRotate));
    }
}