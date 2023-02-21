using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

public class SteamworksManager : MonoBehaviour
{
	public static SteamworksManager Instance { get; private set; } = null;
	private FacepunchTransport transport;
	public Lobby? CurrentLobby { get; private set; } = null;

	#region Unity Functions

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
    {
		transport = NetworkManager.Singleton.GetComponent<FacepunchTransport>();

        SteamMatchmaking.OnLobbyCreated += OnLobbyCreated;
        SteamMatchmaking.OnLobbyEntered += OnLobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined += OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave += OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyInvite += OnLobbyInvite;
        SteamFriends.OnGameLobbyJoinRequested += OnGameLobbyJoinRequested;
	}

    private void OnDestroy()
	{
		SteamMatchmaking.OnLobbyCreated -= OnLobbyCreated;
		SteamMatchmaking.OnLobbyEntered -= OnLobbyEntered;
		SteamMatchmaking.OnLobbyMemberJoined -= OnLobbyMemberJoined;
		SteamMatchmaking.OnLobbyMemberLeave -= OnLobbyMemberLeave;
		SteamMatchmaking.OnLobbyInvite -= OnLobbyInvite;
		SteamFriends.OnGameLobbyJoinRequested -= OnGameLobbyJoinRequested;
	}

	private void OnApplicationQuit() => Disconnect();

	#endregion

	public async void StartHost(uint maxMembers)
	{
		NetworkManager.Singleton.StartHost();

		CurrentLobby = await SteamMatchmaking.CreateLobbyAsync((int)maxMembers);
    }

	public void StartClient(SteamId id)
	{
		transport.targetSteamId = id;

		Debug.Log($"Joining room hosted by {transport.targetSteamId}", this);

		if (NetworkManager.Singleton.StartClient())
		{
			Debug.Log("Client has joined!", this);
		}	
	}

	public void Disconnect()
	{
		CurrentLobby?.Leave();

		if (NetworkManager.Singleton == null) return;

		NetworkManager.Singleton.Shutdown();
	}

	#region Steam Callbacks

	private void OnGameLobbyJoinRequested(Lobby lobby, SteamId id)
	{
		bool isSame = lobby.Owner.Id.Equals(id);

		Debug.Log($"Owner: {lobby.Owner}");
		Debug.Log($"Id: {id}");
		Debug.Log($"IsSame: {isSame}", this);

		StartClient(id);
	}

	private void OnLobbyInvite(Friend friend, Lobby lobby) => Debug.Log($"You got a invite from {friend.Name}", this);

	private void OnLobbyMemberLeave(Lobby lobby, Friend friend) { }

	private void OnLobbyMemberJoined(Lobby lobby, Friend friend) { }

	private void OnLobbyEntered(Lobby lobby)
    {
		Debug.Log($"You have entered in lobby, clientId={NetworkManager.Singleton.LocalClientId}", this);

		if (NetworkManager.Singleton.IsHost) return;

		StartClient(lobby.Owner.Id);
	}

    private void OnLobbyCreated(Result result, Lobby lobby)
	{
		if (result != Result.OK)
        {
			Debug.LogError($"Lobby couldn't be created!, {result}", this);
			return;
		}

		lobby.SetFriendsOnly();
		lobby.SetData("name", "Default Lobby Name");
		lobby.SetJoinable(true);

		Debug.Log("Lobby has been created!");
	}

	#endregion
}