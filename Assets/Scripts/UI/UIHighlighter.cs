using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace markow
{ 
    [RequireComponent(typeof(Image))]
    public class UIHighlighter : MonoBehaviour
    {
        private Image img;
        private float alphaValue;

        private void Awake()
        {
            img = GetComponent<Image>();
            alphaValue = 0f;
        }

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            DOTween.To(() => alphaValue, x => alphaValue = x, .5f, .25f)
            .OnUpdate(() => {
                img.color = new Color(1, 1, 1, alphaValue);
            });
        }

        public void Hide() 
        {
            DOTween.To(() => alphaValue, x => alphaValue = x, 0f, .25f)
            .OnUpdate(() => {
                img.color = new Color(1, 1, 1, alphaValue);
            }); 
        }

    }
}