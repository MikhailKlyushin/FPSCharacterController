using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputKeyAndMouse : IInputProvider, IInitializable
{
    private StarterAssets _inputControl;

    private Vector2 _movePosition;
    private Vector2 _rotatePosition;

    private float _rotationX;
    private float _rotationY;
    
    private readonly SignalBus _signalBus;
    private SignalInputProvider _input;
    
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    private InputKeyAndMouse(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _inputControl = new StarterAssets();
        _inputControl.Enable();
        Start();
    }

    private void Start()
    {
        SubscribeToKeyPress();
        
        Observable.EveryFixedUpdate() // поток update
            .Subscribe(_ =>
            {
                GetInputVectors();
                SetMoveAndRotatePosition();
            }).AddTo(_disposable);
    }

    private void SubscribeToKeyPress()
    {
        //set keys
    }
    
    private void GetInputVectors()
    {
        _movePosition = _inputControl.Player.Move.ReadValue<Vector2>();
        _rotatePosition = _inputControl.Player.Look.ReadValue<Vector2>();
    }

    private void SetMoveAndRotatePosition()
    {
        //TODO: rewrite input
        //_input.PositionToMove = new Vector3(_movePosition.x, 0, _movePosition.y);
        _input.PositionToMove = Vector3.Lerp(_input.PositionToMove, new Vector3(_movePosition.x, 0, _movePosition.y), 0.2f);
        
        _input.PositionToRotate = new Vector3(_rotatePosition.y, _rotatePosition.x, 0);

        InputEventNotification();
    }

    private void InputEventNotification() => _signalBus.Fire(_input);
}