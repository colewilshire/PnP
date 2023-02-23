using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectionHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public static event Action<CharacterSelectionHandler> CharacterSelected;

	public void OnPointerDown (PointerEventData eventData) 
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log ($"{this.gameObject.name} was {eventData.button.ToString()} clicked.");
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
			CharacterSelected?.Invoke(this);
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
