using System;
using UnityEngine;

public class CharacterModel
{
    #region Public params

    public Guid CharacterID => _characterID;

    public float MoveSpeed => _moveSpeed;

    public Vector3 Position => _character.transform.position;
    public Vector3 Velocity => _velocity;

    public Vector3 LocalRotate => _localRotate;
    public Vector3 LocalRotateAngleX => _rotationVectorX;
    public Vector3 LocalRotateAngleY => _character.transform.localEulerAngles;
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

    private CharacterConfig _config;
    private IInputProvider _input;

    private Vector3 _localRotate;
    private Vector3 _rotationVectorX;


    public CharacterModel(IInputProvider inputController, string pathToConfigFile)
    {
        SetConfigParams(pathToConfigFile);
        ConnectInputController(inputController);
    }
    
    private void SetConfigParams(string path)
    {
        _config = Resources.Load<CharacterConfig>(path);

        _moveSpeed = _config.MoveSpeed;
        _sensivityHorisontal = _config.SensivityHorizontal;
        _sensivityVertical = _config.SensivityVertical;
        _minimumVerticalAngle = _config.MinimumVerticalAngle;
        _maximumVerticalAngle = _config.MaximumVerticalAngle;
    }
    
    private void ConnectInputController(IInputProvider input)
    {
        _input = input;
        _input.InputNotify += ChangeCharacterPosition;

        _input.UpdateInput();   // запуск асинхронного метода
    }


    private Vector3 _inputVector;
    private Vector3 _positionToMove;
    private Vector3 _positionToRotate;

    private void ChangeCharacterPosition(Vector3 positionToMove, Vector3 positionToRotate)
    {
        _inputVector = positionToMove;  // сохраняем вектор ввода (для управления анимацией)
        _positionToMove = positionToMove;
        _positionToRotate = positionToRotate;

        MoveToPosition();
        RotateToPosition();
    }
    
    private GameObject _character = new GameObject();
    private Vector3 _velocity;

    private void MoveToPosition()
    {
        if (_character != null)
        {
            _positionToMove = _character.transform.TransformDirection(_positionToMove);
            _velocity = _positionToMove;
            //_velocity = Vector3.Normalize(_velocity);     // при активации появляется инпут лаг
            _velocity *= _moveSpeed;
        }
    }
    
    private float _rotationPositionX;
    private float _rotationPositionY;
    private float _delta;

    private void RotateToPosition()
    {
        if (_character != null)
        {
            _rotationPositionX -= _positionToRotate.x * _sensivityVertical;
            _rotationPositionX = Mathf.Clamp(_rotationPositionX, _minimumVerticalAngle, _maximumVerticalAngle);

            _delta = _positionToRotate.y * _sensivityHorisontal;
            _rotationPositionY = _character.transform.localEulerAngles.y + _delta;

            _rotationVectorX = new Vector3(_rotationPositionX, 0, 0);

            var _rotationVectorY = new Vector3(0, _rotationPositionY, 0);
            _character.transform.localEulerAngles = _rotationVectorY;

            _localRotate = new Vector3(_rotationPositionX, _rotationPositionY, 0);
        }
    }
}