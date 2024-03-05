using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace markow
{ 
    [RequireComponent(typeof(Image))]
    public class UIHighlighter : MonoBehaviour
    {
        private void Awake()
        {
            
        }

        public void Show()
        {
            Debug.Log("[UIHighlighter] Show");
        }

        public void Hide() 
        {
            Debug.Log("[UIHighlighter] Hide");
        }

    }
}