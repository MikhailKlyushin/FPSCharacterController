using UniRx;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(NetworkAnimator))]

public class BaseCharacterNetworkView : NetworkBehaviour, IIdentified
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public NetworkVariable<Vector3> Velocity = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> Rotate = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public NetworkVariable<float> DirectionHorizontal = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> DirectionVertical = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
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

    public void SetID(string id)
    {
        _characterID = id;
    }

    public void SetCharacterMove(Vector3 velocity, Quaternion rotate)
    {
        _rigidbody.velocity = transform.TransformDirection(velocity);
        Debug.DrawRay(transform.position, velocity, Color.green);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 0.4f);
    }

    public void SetAnimatorParams(float directionHorizontal, float directionVertical, float speed)
    {
        var networkAnimator = GetComponent<NetworkAnimator>();
        networkAnimator.Animator.speed = speed;
        
        _animator.SetFloat(_horizontal, directionHorizontal);
        _animator.SetFloat(_vertical, directionVertical);
        _animator.speed = speed;
    }
}
