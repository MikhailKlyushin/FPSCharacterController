using Unity.Netcode;
using UnityEngine;
using Zenject;


public class NonPooledDynamicSpawner : NetworkBehaviour
{
    //public GameObject PrefabToSpawn;
    public bool DestroyWithSpawner;        
    private GameObject m_PrefabInstance;
    private NetworkObject m_SpawnedNetworkObject;
    private CharacterService _playerService;
    
    [Inject]
    public void Construct(CharacterService service)
    {
        _playerService = service;
    }


    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += clientId =>
        {
            var playerID = _playerService.CreatePlayer();
            var playerView = _playerService.GetView(playerID);
            var cameraView = _playerService.CreatePlayerCamera(playerView.transform);
            Debug.Log("id = " + playerID);
            m_PrefabInstance = playerView.gameObject;

            // Optional, this example applies the spawner's position and rotation to the new instance
            m_PrefabInstance.transform.position = transform.position;
            m_PrefabInstance.transform.rotation = transform.rotation;

            // Get the instance's NetworkObject and Spawn
            m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
            m_SpawnedNetworkObject.SpawnWithOwnership(clientId);
        };
    }
    /*// Only the server spawns, clients will disable this component on their side
    enabled = IsServer;            
    if (!enabled) //|| PrefabToSpawn == null)
    {
        return;
    }
    // Instantiate the GameObject Instance
    var playerID = _playerService.CreatePlayer();
    var playerView = _playerService.GetView(playerID);
    var cameraView = _playerService.CreatePlayerCamera(playerView.transform);
    Debug.Log(playerID);
    m_PrefabInstance = playerView.gameObject;
        
    // Optional, this example applies the spawner's position and rotation to the new instance
    m_PrefabInstance.transform.position = transform.position;
    m_PrefabInstance.transform.rotation = transform.rotation;
        
    // Get the instance's NetworkObject and Spawn
    m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
    m_SpawnedNetworkObject.Spawn();
}*/

    public override void OnNetworkDespawn()
    {
        if (DestroyWithSpawner && m_SpawnedNetworkObject != null && m_SpawnedNetworkObject.IsSpawned)
        {
            m_SpawnedNetworkObject.Despawn();
        }
        base.OnNetworkDespawn();
    }
}
