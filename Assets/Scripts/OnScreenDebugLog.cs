using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OnScreenDebug
{
    public class OnScreenDebugLog : MonoBehaviour
    {
        public static OnScreenDebugLog Instance {get; private set;}
        private List<string> messageList = new List<string>();

        #region Unity Functions

        private void Start()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnGUI()
        {
            PrintMessages();
        }

        #endregion

        public void AddOnScreenDebugMessage(string message, float? timeToDisplay = null)
        {
            messageList.Add(message);

            if (timeToDisplay.HasValue)
            {
                StartCoroutine(RemoveOnScreenDebugMessage(message, timeToDisplay.GetValueOrDefault()));
            }
        }

        private IEnumerator RemoveOnScreenDebugMessage(string message, float timeToDisplay)
        {
            yield return new WaitForSeconds(timeToDisplay);
            
            messageList.RemoveAt(messageList.Count - 1);
        }

        private void PrintMessages()
        {
            foreach (string message in messageList)
            {
                GUILayout.Label(message);
            }
        }
    }
}
