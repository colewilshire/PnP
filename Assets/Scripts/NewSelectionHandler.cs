//using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OnScreenDebug;

public class NewSelectionHandler : NetworkBehaviour
{
    [SerializeField] private List <DynastyCharacter> selectedCharacters = new List<DynastyCharacter>();
    //public event Action CharacterSelected;
    //public event Action Move;
    [SerializeField] private Camera followCamera;

    // Start is called before the first frame update
    private void Start()
    {
        OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"My owner is: {OwnerClientId}");
        if (!IsOwner) return;
        followCamera = gameObject.GetComponentInChildren<Camera>();
    }

    public void SelectCharacter(DynastyCharacter character)
    {
        //OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"Selected {character.OwnerClientId}", 5);

        selectedCharacters.Remove(character);
        selectedCharacters.Add(character);
    }

    public void DeselectCharacter(DynastyCharacter character)
    {
        //OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"Deelected {character.OwnerClientId}", 5);

        selectedCharacters.Remove(character);
    }

    private void Update()
    {
        //foreach (DynastyCharacter character in selectedCharacters)
        //{
        //    if (!character.IsOwner) return;
            //character.Move();
        //}

        //CharacterSelected?.Invoke();
        //Move.Invoke();
        if (!IsOwner) return;
        RaycastFromCamera();
    }

    private void RaycastFromCamera()
    {
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
        
        foreach (DynastyCharacter character in selectedCharacters)
        {
            if (character.IsOwner)
            {
                //OnScreenDebugLog.Instance.AddOnScreenDebugMessage($"Moving: {character.OwnerClientId}", .01f);
                character.Move(hit.point);
            }
        }
    }
}
