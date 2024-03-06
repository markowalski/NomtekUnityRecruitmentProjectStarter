using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace markow
{
    public class GridItem : MonoBehaviour
    {
        public class OnGridItemSelectedEv : UnityEvent<ENTITY_TYPE> { }
        public OnGridItemSelectedEv OnGridItemSelectedEvDispatcher = new OnGridItemSelectedEv();

        [SerializeField]
        private Image img;
        [SerializeField]
        private TMPro.TextMeshProUGUI desc;
        private ENTITY_TYPE type;

        public void Setup(GridItemSO _gridItemSO)
        {
            img.sprite = _gridItemSO.Sprite;
            desc.text = _gridItemSO.ItemName;
            type = _gridItemSO.Type;
        }

        public void OnPointerClickedHandler()
        {
            OnGridItemSelectedEvDispatcher?.Invoke(type);
        }
    }
}