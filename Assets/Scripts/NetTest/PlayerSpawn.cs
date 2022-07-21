using Unity.Netcode;
using UnityEngine;

public class PlayerSpawn : NetworkBehaviour
{
    [SerializeField] private CharacterConfig _playerConfig;
    private NetworkObject _spawnedPlayerObject;
    //private ModelForNetworkPlayer _netModel;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        
        Debug.Log("Client owner = " + OwnerClientId);
        _spawnedPlayerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(OwnerClientId);
        var playerObject = _spawnedPlayerObject.gameObject;

        //var ownerId = playerObject.GetComponent<ClientIsOwner>().OwnerId;

        //var cameraView = _playerService.CreatePlayerCamera(playerObject.transform);
        var model = new CharacterModel(new InputKeyAndMouse(), _playerConfig);
        Debug.Log("Model ID = " + model.ID);
        var view = playerObject.GetComponent<CharacterView>();
        view.SetModel(model);
        Debug.Log("View ID = " + view.ID);

    }
}
