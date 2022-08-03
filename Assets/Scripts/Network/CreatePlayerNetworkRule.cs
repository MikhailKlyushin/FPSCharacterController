using Cinemachine;
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
            Debug.Log("Client ID = " + clientId);
            
            _spawnedPlayerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientId);
            _playerObject = _spawnedPlayerObject.gameObject;

            SetParamsNetworkPlayerCharacter();
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += clientId =>
        {
            if ((_clientId == clientId) && (_spawnedPlayerObject != null) && _spawnedPlayerObject.IsSpawned)
            {
                _spawnedPlayerObject.Despawn();
            }
        };
    }
    
    private void SetParamsNetworkPlayerCharacter()
    {
        //var cameraView = _playerService.CreatePlayerCamera(playerObject.transform);
        var model = _characterService.CreateAndGetModelForNetworkPlayer();
        var view = _playerObject.GetComponent<CharacterNetworkView>();
        view.SetModel(model);
                
        Debug.Log("Model ID = " + model.ID);
        Debug.Log("View ID = " + view.ID);
    }
}
