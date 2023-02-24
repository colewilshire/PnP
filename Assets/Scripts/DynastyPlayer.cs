using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OnScreenDebug;

public class DynastyPlayer : NetworkBehaviour
{
    [SerializeField] private DynastyCharacter dynastyCharacterPrefab;
    [SerializeField] private DynastyCharacter selectedCharacter;
    [SerializeField] private Camera followCamera;
    public event Action CharacterSelected;

    private void Start()
    {
        if (!IsOwner) return;
        followCamera = GetComponentInChildren<Camera>();
        CreateCharacterServerRpc();
    }

    [ServerRpc]
    private void CreateCharacterServerRpc(ServerRpcParams rpcParams = default)
    {
        DynastyCharacter newCharacter = Instantiate(dynastyCharacterPrefab);
        newCharacter.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }

    /*
    [SerializeField] private List<DynastyCharacter> dynastyMembers = new List <DynastyCharacter>();
    [SerializeField] private DynastyCharacter dynastyCharacterPrefab;

    private void Start()
    {
        OnScreenDebugLog.Instance.AddOnScreenDebugMessage("Start");
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        gameObject.name = $"Dynasty {OwnerClientId.ToString()}";
        CreateCharacter();
        CreateCharacterServerRpc();
    }

    private void CreateCharacter()
    {
        DynastyCharacter newCharacter = Instantiate(dynastyCharacterPrefab);
        newCharacter.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
        dynastyMembers.Add(newCharacter);

        if (NetworkManager.Singleton.IsServer)
        {
            OnScreenDebugLog.Instance.AddOnScreenDebugMessage("I am server");
        }

        if (NetworkManager.Singleton.IsClient)
        {
            OnScreenDebugLog.Instance.AddOnScreenDebugMessage("I am client");
        }

        OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"I am: {OwnerClientId.ToString()}");
    }

    [ServerRpc]
    private void CreateCharacterServerRpc(ServerRpcParams rpcParams = default)
    {
        OnScreenDebugLog.Instance.AddOnScreenDebugMessage("Serve RPC triggered.");
        CreateCharacterClientRpc();
    }

    [ClientRpc]
    private void CreateCharacterClientRpc(ClientRpcParams rpcParams = default)
    {
        if (IsOwner) return;
        OnScreenDebugLog.Instance.AddOnScreenDebugMessage("Client RPC triggered.");
        CreateCharacter();
    }
    */
}
