using Unity.Netcode;
using Steamworks;
using Steamworks.Data;
using HelloWorld;
using UnityEngine;

public class NetworkingButtonsManager : MonoBehaviour
{
    [SerializeField] public SteamworksManager steamworksManager;

    #region Unity Functions

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();

            SubmitNewPosition();
        }

        GUILayout.EndArea();
    }

    #endregion

    private void StartButtons()
    {
        if (GUILayout.Button("Host") && steamworksManager)
        {
            steamworksManager.StartHost(8);
        }
    }

    private void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        if (GUILayout.Button("Invite Friends") && steamworksManager)
        {
            Lobby? currentLobby = steamworksManager.CurrentLobby;

            if (currentLobby.HasValue)
            {
                SteamId lobbyId = currentLobby.GetValueOrDefault().Id;
                SteamFriends.OpenGameInviteOverlay(lobbyId);
            }
        }
    }

    static private void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient )
            {
                foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                {
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
                }
            }
            else
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();
            }
        }
    }
}