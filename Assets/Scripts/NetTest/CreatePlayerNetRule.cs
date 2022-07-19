using Unity.Netcode;
using UnityEngine;
using Zenject;

public class CreatePlayerNetRule : IInitializable
{
    private GameObject m_PrefabInstance;
    private NetworkObject m_SpawnedNetworkObject;
    private CharacterService _playerService;
    
    [Inject]
    public void Construct(CharacterService service)
    {
        _playerService = service;
    }

    public void Initialize()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += clientId =>
        {
            var playerID = _playerService.CreatePlayer();
            var playerView = _playerService.GetView(playerID);
            var cameraView = _playerService.CreatePlayerCamera(playerView.transform);
            Debug.Log("id = " + playerID);
            var playerObject = playerView.gameObject;

            // Optional, this example applies the spawner's position and rotation to the new instance
            playerObject.transform.position = Vector3.zero;
            playerObject.transform.rotation = new Quaternion();

            // Get the instance's NetworkObject and Spawn
            m_SpawnedNetworkObject = playerObject.GetComponent<NetworkObject>();
            m_SpawnedNetworkObject.SpawnWithOwnership(clientId);
        };
    }
}
