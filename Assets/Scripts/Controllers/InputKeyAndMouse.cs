using UniRx;
using UnityEngine;
using Zenject;

public class InputKeyAndMouse : IInputProvider, IInitializable
{
    public ReadOnlyReactiveProperty<Vector2> MovePosition => _movePosition.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<Vector2> RotatePosition => _rotatePosition.ToReadOnlyReactiveProperty();
    
    private readonly ReactiveProperty<Vector2> _movePosition = new ReactiveProperty<Vector2>();
    private readonly ReactiveProperty<Vector2> _rotatePosition = new ReactiveProperty<Vector2>();
    
    private readonly CompositeDisposable _disposable = new CompositeDisposable();
    
    private StarterAssets _inputControl;
    

    public void Initialize()
    {
        _inputControl = new StarterAssets();
        _inputControl.Enable();
        
        Observable.EveryFixedUpdate()
            .Subscribe(_ =>
            {
                var nextMovePosition = _inputControl.Player.Move.ReadValue<Vector2>();
                _movePosition.SetValueAndForceNotify(Vector2.Lerp(_movePosition.Value, nextMovePosition, 0.2f));
                
                _rotatePosition.SetValueAndForceNotify(_inputControl.Player.Look.ReadValue<Vector2>());
            }).AddTo(_disposable);
    }
}