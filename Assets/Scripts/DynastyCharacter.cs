using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OnScreenDebug;

public class DynastyCharacter : NetworkBehaviour
{
    //[SerializeField] public NetworkVariable<string> CharacterName = new NetworkVariable<string>("New");
    //[SerializeField] public NetworkVariable<string> DynastyName = new NetworkVariable<string>("Character");
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    #region Unity Functions

    private void OnEnable()
    {
        Position.OnValueChanged += OnPositionChanged;
    }

    private void OnDisable()
    {
        Position.OnValueChanged -= OnPositionChanged;
    }

    private void Start()
    {
        NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        DynastyPlayer player = playerObject.GetComponent<DynastyPlayer>();
    
        //player.CharacterSelected += OnCharacterSelected;
    }

    #endregion

    //private void OnCharacterSelected()
    //{
        //if (!IsOwner) return;
        //Move();
    //}

    private void OnPositionChanged(Vector3 oldPosition, Vector3 newPosition)
    {
        OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"{OwnerClientId} triggered on position changed.", .1f);

        transform.position = newPosition;
    }

    public void Move(Vector3 newPosition)
    {
        //Position.Value = newPosition;

        MoveServerRpc(newPosition);
    }

    #region Server Functions

    [ServerRpc]
    private void MoveServerRpc(Vector3 newPosition)
    {
        //if (IsOwner) return;
        //Move(newPosition);

        Position.Value = newPosition;
    }

    // [ClientRpc]
    // private void MoveClientRpc(Vector3 newPosition)
    // {
    //     //if (IsOwner) return;
    //     //Move(newPosition);

    //     Position.Value = newPosition;
    // }

    #endregion
}