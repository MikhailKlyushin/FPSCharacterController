using UniRx;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;


[RequireComponent(typeof(CharacterNetworkView))]
[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(NetworkAnimator))]

public class GhostCharacterNetworkView : NetworkBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            return;
        }

        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        var view = GetComponent<CharacterNetworkView>();
        var networkAnimator = GetComponent<NetworkAnimator>();
        networkAnimator.Animator.speed = 3f;
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            SetCharacterMove(view._velocity.Value, view._rotate.Value);
            SetAnimatorParams(view._directionHorizontal.Value, view._directionVertical.Value, 3f);
        }).AddTo(_disposables);
    }
    
    private void SetCharacterMove(Vector3 velocity, Quaternion rotate)
    {
        _rigidbody.velocity = transform.TransformDirection(velocity);
        Debug.DrawRay(transform.position, velocity, Color.green);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 0.4f);
    }

    private void SetAnimatorParams(float directionHorizontal, float directionVertical, float speed)
    {
        _animator.SetFloat(_horizontal, directionHorizontal);
        _animator.SetFloat(_vertical, directionVertical);
        _animator.speed = speed;
    }
}