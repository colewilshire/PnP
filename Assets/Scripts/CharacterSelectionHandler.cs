using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectionHandler : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public bool isSelected {get; private set;}
    private DynastyPlayer player;
	//public static event Action CharacterSelected;

	// private void Enable()
	// {
	// 	CharacterSelected += OnCharacterSelected;
	// }

	// private void Disable()
	// {
	// 	CharacterSelected -= OnCharacterSelected;
	// }

	// private void OnCharacterSelected()
	// {

	// }

	private void Start()
    {
        NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        player = playerObject.GetComponent<DynastyPlayer>();
    }

	public void OnPointerDown (PointerEventData eventData) 
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log ($"{this.gameObject.name} was {eventData.button.ToString()} clicked.");

			//CharacterSelected?.Invoke();
		}

		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log ($"{this.gameObject.name} was {eventData.button.ToString()} clicked.");
		}
	}

    public void OnPointerUp (PointerEventData eventData) 
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log ($"The {eventData.button} mouse button was released.");
			//CharacterSelected?.Invoke(this);
		}

		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log ($"The {eventData.button} mouse button was released.");
		}
	}

	public void OnPointerEnter (PointerEventData eventData) 
	{
		Debug.Log ("The cursor entered the selectable UI element.");
	}

	public void OnPointerExit (PointerEventData eventData) 
	{
		Debug.Log ("The cursor exited the selectable UI element.");
	}
}
