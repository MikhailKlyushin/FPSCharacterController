using UniRx;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]

public class BaseCharacterNetworkView : NetworkBehaviour, IIdentified
{
    public string ID => _characterID;

    private string _characterID;

    private Rigidbody _rigidbody;

    private Animator _animator;

    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");

    protected readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    public override void OnDestroy()
    {
        _disposables.Dispose();
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    protected void SetID(string id)
    {
        _characterID = id;
    }

    protected void SetSyncCharacterMove(Vector3 velocity, Quaternion rotate)
    {
        _rigidbody.velocity = transform.TransformDirection(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 0.4f);
    }
    
    protected void SetSyncCharacterPosition(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.4f);
    }

    protected void SetSyncAnimatorParams(float directionHorizontal, float directionVertical, float speed)
    {
        _animator.SetFloat(_horizontal, directionHorizontal);
        _animator.SetFloat(_vertical, directionVertical);
        _animator.speed = speed;
    }
}
