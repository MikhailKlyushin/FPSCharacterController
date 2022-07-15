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
    
    //TODO: use the reactive properties, they're faster than the signals
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
        //TODO: It's empty
        SubscribeToKeyPress();
        
        //TODO: Russian comments again
        //TODO: It's useless because you can to subscribe on the perform input event (see todo in GetInputVectors()
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
        //TODO: Try like this:
        // _inputControl.Player.Move.performed +=
        //         context => reactiveProperty.SetValueAndForceNotify(context.ReadValue<Vector2>()); 
        
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