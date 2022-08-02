using Unity.Netcode;
using UnityEngine;
using Zenject;

public class CreateClientPlayerNetworkRule : IInitializable
{
    private CharacterService _characterService;
    private NetworkObject _spawnedPlayerObject;
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
            Debug.Log("Client ID = " + clientId);
            _spawnedPlayerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientId);
            var playerObject = _spawnedPlayerObject.gameObject;
            
            var clientComponent = playerObject.GetComponent<ClientOwnerNetworkComponent>();

            if (clientId == clientComponent.OwnerClientId)
            {
                //var cameraView = _playerService.CreatePlayerCamera(playerObject.transform);
                var model = _characterService.CreateAndGetModelForNetPlayer();
                var view = playerObject.GetComponent<CharacterNetworkView>();
                view.SetModel(model);
                
                Debug.Log("Model ID = " + model.ID);
                Debug.Log("View ID = " + view.ID);
            }
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
