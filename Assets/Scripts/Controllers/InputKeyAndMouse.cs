using UniRx;
using UnityEngine;
using Zenject;

public class InputKeyAndMouse : IInputProvider, IInitializable
{
    public ReactiveProperty<Vector3> MovePosition { get; } = new ReactiveProperty<Vector3>();
    public ReactiveProperty<Vector3> RotatePosition { get; } = new ReactiveProperty<Vector3>();
    
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
                _movePosition.SetValueAndForceNotify(_inputControl.Player.Move.ReadValue<Vector2>());
                _rotatePosition.SetValueAndForceNotify(_inputControl.Player.Look.ReadValue<Vector2>());
            }).AddTo(_disposable);

        FormatInputVectors();
    }

    private void FormatInputVectors()
    {
        _movePosition.Subscribe(vector2 =>
        {
            MovePosition.SetValueAndForceNotify(Vector3.Lerp(MovePosition.Value, new Vector3(vector2.x, 0, vector2.y), 0.2f));
        }).AddTo(_disposable);
        
        _rotatePosition.Subscribe(vector2 =>
        {
            RotatePosition.SetValueAndForceNotify(new Vector3(vector2.y, vector2.x, 0));
        }).AddTo(_disposable);
    }
}