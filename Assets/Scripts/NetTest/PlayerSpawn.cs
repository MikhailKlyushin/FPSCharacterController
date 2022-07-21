using Unity.Netcode;
using UnityEngine;

public class PlayerSpawn : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        Debug.Log("Client Owner player = " + OwnerClientId);
    }
}
