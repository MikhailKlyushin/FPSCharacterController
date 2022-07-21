using Unity.Netcode;
using UnityEngine;
using Zenject;

public class CreatePlayerNetRule : IInitializable
{
    private CharacterService _playerService;
    private NetworkObject _spawnedPlayerObject;
    private ulong _clientId;

    [Inject]
    public void Construct(CharacterService service)
    {
        _playerService = service;
    }

    public void Initialize()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += clientId =>
        {
            _clientId = clientId;
            Debug.Log("Client ID = " + clientId);
            _spawnedPlayerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientId);
            var playerObject = _spawnedPlayerObject.gameObject;
            
            var clientComponent = playerObject.GetComponent<ClientIsOwner>();

            if (clientId == clientComponent.OwnerClientId && clientComponent.IsLocalPlayer)
            {
                //var cameraView = _playerService.CreatePlayerCamera(playerObject.transform);
                var model = _playerService.CreateAndGetModelForNetPlayer();
                Debug.Log("Model ID = " + model.ID);
                var view = playerObject.GetComponent<CharacterView>();
                view.SetModel(model);
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
