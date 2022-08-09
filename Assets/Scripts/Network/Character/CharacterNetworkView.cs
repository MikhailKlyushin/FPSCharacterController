using UniRx;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterNetworkState))]
public class CharacterNetworkView : NetworkBehaviour
{
    private CharacterNetworkState _state;
    private INetworkViewThread _thread;


    public override void OnNetworkSpawn()
    {
        _state = GetComponent<CharacterNetworkState>();
        
        if (IsOwner)
        {
            this.gameObject.AddComponent<WriteNetworkViewThread>();
        }
        else            // server
        {
            this.gameObject.AddComponent<ReadNetworkViewThread>();
            _thread = GetComponent<ReadNetworkViewThread>();
            _thread.StartThread(_state);
        }
    }


    public void SetModel(ICharacterModel model)
    {
        if (IsOwner)    // client
        {
            _thread = GetComponent<WriteNetworkViewThread>();
            _thread.SetModel(model);
            _thread.StartThread(_state);
        }
    }
}
