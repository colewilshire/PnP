using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }

        public void PrintThing()
        {
            Debug.Log("qqq");
        }
    }
}