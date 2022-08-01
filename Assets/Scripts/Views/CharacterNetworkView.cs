using System;
using UniRx;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(CharacterView),typeof(Rigidbody), typeof(Animator))]

public class CharacterNetworkView : NetworkBehaviour, IIdentified
{
    public string ID => _characterID;
    private string _characterID;
    
    private CharacterView _view;
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    

    /*private NetworkVariable<Vector3> _velocity = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<Quaternion> _rotate = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> _directionHorizontal = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> _directionVertical = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    

    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public override void OnDestroy()
    {
        _disposables.Dispose();
    }

    private void Start()
    {
        _view = GetComponent<CharacterView>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        var netAnimator = GetComponent<NetworkAnimator>();
        netAnimator.Animator.speed = _view.CharacterState.SpeedAnimation;

        _characterID = _view.ID;

        if (!IsOwner)
        {
            _view.enabled = false;
        }
    }

    public override void OnNetworkSpawn()
    {
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (IsOwner)
            {
                _velocity.Value = _view.CharacterState.Velocity;
                _rotate.Value = _view.CharacterState.Rotate;
                _directionHorizontal.Value = _view.CharacterState.DirectionHorizontal;
                _directionVertical.Value = _view.CharacterState.DirectionVertical;
            }
            else
            {
                _rigidbody.velocity = transform.TransformDirection(_velocity.Value);
                Debug.DrawRay(transform.position, _velocity.Value, Color.green);
            
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotate.Value, 0.4f);

                SetAnimatorParams();
            }
        }).AddTo(_disposables);
    }


    private void SetAnimatorParams()
    {
        _animator.SetFloat(_horizontal, _directionHorizontal.Value);
        _animator.SetFloat(_vertical, _directionVertical.Value);
        _animator.speed = _view.CharacterState.SpeedAnimation;
    }*/
}
