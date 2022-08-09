using UniRx;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterNetworkParams))]
public class CharacterNetworkView : NetworkBehaviour
{
    public string ID => _characterID;

    private string _characterID;

    private Rigidbody _rigidbody;

    private Animator _animator;

    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");

    protected readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    private CharacterNetworkParams _state;
    private INetworkViewThread _thread;
    


    public override void OnNetworkSpawn()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _state = GetComponent<CharacterNetworkParams>();
        
        if (!IsOwner)   // server
        {
            _thread =  new ReadNetworkViewThread();
            _thread.StartThread(transform, _rigidbody, _animator, _state);
        }
    }


    public void SetModel(ICharacterModel model)
    {
        if (IsOwner)    // client
        {
            _thread = new WriteNetworkViewThread();
            _thread.SetModel(model);
            _thread.StartThread(transform, _rigidbody, _animator, _state);
        }
    }
}
