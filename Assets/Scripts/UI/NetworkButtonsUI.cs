using Unity.Netcode;
using UnityEngine;

public class NetworkButtonsUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Host", GUILayout.Height(40)))
            {
                NetworkManager.Singleton.StartHost();
            }
            
            if (GUILayout.Button("Server", GUILayout.Height(40)))
            {
                NetworkManager.Singleton.StartServer();
            }
            
            if (GUILayout.Button("Client", GUILayout.Height(40)))
            {
                NetworkManager.Singleton.StartClient();
            }
        }
        
        GUILayout.EndArea();
    }
}
