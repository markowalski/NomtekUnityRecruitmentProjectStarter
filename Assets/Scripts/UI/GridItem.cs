using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace markow
{
    public class GridItem : MonoBehaviour
    {
        private Image img;
        private TMPro.TextMeshProUGUI desc;

        public void Setup(GridItemSO _gridItemSO)
        {
            img.sprite = _gridItemSO.sprite;
            desc.text = _gridItemSO.name;
        }

        public void OnPointerClickedHandler()
        {

        }
    }
}