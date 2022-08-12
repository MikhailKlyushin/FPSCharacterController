using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkHealth : NetworkBehaviour
{
    [SerializeField] private CharacterConfig _config;

    private CharacterNetworkParams _state;

    private short _health = 0;

    public override void OnNetworkSpawn()
    {
        _health = _config.MaxHealth;
        _state = GetComponent<CharacterNetworkParams>();
        
        if (IsOwner)
        {
            _state.Health.Value = _health;
        }
    }

    public void Remove(short value)
    {
        if (!IsServer)  // client
        {
            GetDamageServerRPC(value);
        }
        else            // server
        {
            GetDamageClientRPC(value);
        }
            
    }

    [ServerRpc(RequireOwnership = false)]
    private void GetDamageServerRPC(short value)
    {
        GetDamage(value);
    }

    [ClientRpc]
    private void GetDamageClientRPC(short value)
    {
        if (IsOwner)
        {
            GetDamage(value);
        }
    }

    private void GetDamage(short value)
    {
        var tmpHealth = _health - value;

        if (tmpHealth < 0)
        {
            _health = 0;
        }
        else
        {
            _health -= value;
        }

        _state.Health.Value = _health;
    }


    private void Update()
    {
        Debug.Log("HelthVariable = " + _state.Health.Value + "  " + OwnerClientId);
    }
}
