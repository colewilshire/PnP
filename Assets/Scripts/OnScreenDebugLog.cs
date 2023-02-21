using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenDebugLog : MonoBehaviour
{
    private List<string> messageList = new List<string>();

    #region Unity Functions

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnGUI()
    {
        PrintMessages();
    }

    #endregion

    public void AddOnScreenDebugMessage(string message, float? timeToDisplay)
    {
        messageList.Add(message);

        if (timeToDisplay.HasValue)
        {
            RemoveOnScreenDebugMessage(message, timeToDisplay.GetValueOrDefault());
        }
    }

    private IEnumerator RemoveOnScreenDebugMessage(string message, float timeToDisplay)
    {
        messageList.Remove(message);

        yield return new WaitForSeconds(timeToDisplay);
    }

    private void PrintMessages()
    {
        foreach (string message in messageList)
		{
		    GUILayout.Label(message);
		}
    }
}
