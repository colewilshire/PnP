using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using OnScreenDebug;

public class DynastyCharacter : NetworkBehaviour
{
    //[SerializeField] public NetworkVariable<string> CharacterName = new NetworkVariable<string>("New");
    //[SerializeField] public NetworkVariable<string> DynastyName = new NetworkVariable<string>("Character");
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> Destination = new NetworkVariable<Vector3>();
    private NavMeshAgent navMeshAgent;

    #region Unity Functions

    private void OnEnable()
    {
        Destination.OnValueChanged += OnDestinationChanged;
    }

    private void OnDisable()
    {
        Destination.OnValueChanged -= OnDestinationChanged;
    }

    private void Start()
    {
        NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        DynastyPlayer player = playerObject.GetComponent<DynastyPlayer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    #endregion

    public void SetDestination(Vector3 newDestination)
    {
        ////Destination.Value = newDestination;
        //SetDestinationServerRpc(newDestination);
        navMeshAgent.destination = newDestination;
    }

    private void OnDestinationChanged(Vector3 oldDestination, Vector3 newDestination)
    {
        //OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"{OwnerClientId} triggered on destination changed.", .1f);

        navMeshAgent.destination = newDestination;
    }

    #region Server Functions

    [ServerRpc]
    private void SetDestinationServerRpc(Vector3 newDestination)
    {
        Destination.Value = newDestination;

        //SetDestinationClientRpc(newDestination);
    }

    #endregion
}