using UniRx;
using UnityEngine;
using Zenject;

public class InputKeyAndMouse : IInputProvider, IInitializable
{
    private float _horizontalPosition;
    private float _verticalPosition;

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
        Start();
    }

    private void Start()
    {
        Observable.EveryFixedUpdate() // поток update
            .Subscribe(x =>
            {
                GetInput();
                SetMoveAndRotatePosition();
            }).AddTo(_disposable);
    }
    
    private void GetInput()
    {
        _horizontalPosition = Input.GetAxis("Horizontal");
        _verticalPosition = Input.GetAxis("Vertical");

        _rotationX = Input.GetAxis("Mouse Y");
        _rotationY = Input.GetAxis("Mouse X");
    }

    private void SetMoveAndRotatePosition()
    {
        _input.PositionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
        _input.PositionToRotate = new Vector3(_rotationX, _rotationY, 0);
        
        InputEventNotification();
    }

    private void InputEventNotification() => _signalBus.Fire(_input);
}