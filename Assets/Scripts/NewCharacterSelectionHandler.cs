using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewCharacterSelectionHandler : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private NewSelectionHandler selectionHandler;

    private void Start()
    {
        NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        selectionHandler = playerObject.GetComponent<NewSelectionHandler>();
    }

    public void OnPointerDown (PointerEventData eventData) 
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			//CharacterSelected?.Invoke();
		}
	}

    public void OnPointerUp (PointerEventData eventData) 
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			selectionHandler.SelectCharacter(gameObject.GetComponent<DynastyCharacter>());
		}

        if (eventData.button == PointerEventData.InputButton.Right)
		{
			selectionHandler.DeselectCharacter(gameObject.GetComponent<DynastyCharacter>());
		}
	}
}
