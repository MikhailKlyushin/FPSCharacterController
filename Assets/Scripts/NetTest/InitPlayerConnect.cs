using Unity.Netcode;
using UnityEngine;

public class InitPlayerConnect : NetworkBehaviour
{
    private void Start()
    {
        Conneted();
    }

    private void Conneted()
    {
        NetworkManager.OnClientConnectedCallback += obj =>
        {
            Debug.Log("client is connected: " + obj);
        };
    }
}
