using Unity.Netcode;
using UnityEngine;
using Zenject;

public class CreateClientPlayerNetworkRule : IInitializable
{
    private CharacterService _characterService;
    private NetworkObject _spawnedPlayerObject;
    private GameObject _playerObject;
    private ulong _clientId;

    [Inject]
    public void Construct(CharacterService service)
    {
        _characterService = service;
    }

    public void Initialize()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += clientId =>
        {
            _clientId = clientId;

            _spawnedPlayerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientId);
            _playerObject = _spawnedPlayerObject.gameObject;

            _playerObject = _characterService.AddParamsForNetworkPlayer(_playerObject);
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += clientId =>
        {
            if ((_clientId == clientId) && (_spawnedPlayerObject != null) && _spawnedPlayerObject.IsSpawned)
            {
                _spawnedPlayerObject.Despawn();
            }
        };
    }
}
