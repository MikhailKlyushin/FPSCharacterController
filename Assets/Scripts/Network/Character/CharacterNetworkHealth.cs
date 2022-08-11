using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkHealth : NetworkBehaviour
{
    [SerializeField] private CharacterConfig _config;

    public short Health;
    
    private CharacterNetworkParams _state;

    void Start()
    {
        _state = GetComponent<CharacterNetworkParams>();
        if (IsOwner)
            _state.Health.Value = _config.MaxHealth;
    }

    /*public void Add(short value)
    {
        var tmpHealth = _state.Health.Value + value;

        if (tmpHealth <= _config.MaxHealth)
        {
            _state.Health.Value += value;
        }
        else
        {
            _state.Health.Value = _config.MaxHealth;
        }
    }*/
    
    public void Remove(short value)
    {
        if (IsOwner)
            RemoveHealthServerRpc(value);
    }

    private void Update()
    {
        Health = _state.Health.Value;
        Debug.Log("Helthlocal = " + _state.Health.Value);
        Debug.Log("Helth = " + _state.Health.Value);
    }
    
    [ServerRpc]
    public void RemoveHealthServerRpc(short value)
    {
        var tmpHealth = _state.Health.Value + value;

        if (tmpHealth <= _config.MaxHealth)
        {
            _state.Health.Value += value;
        }
        else
        {
            _state.Health.Value = _config.MaxHealth;
        }
    }
}
