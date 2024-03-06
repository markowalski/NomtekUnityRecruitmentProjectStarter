using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace markow
{
    /*
     * Class responsible for the logic of a 2D element used to populate GridMenu.
    */
    public class GridItem : MonoBehaviour
    {
        // Event notifying other components that the object was clicked. The event passes the object's type as an argument.
        public class OnGridItemSelectedEv : UnityEvent<ENTITY_TYPE> { }
        public OnGridItemSelectedEv OnGridItemSelectedEvDispatcher = new OnGridItemSelectedEv();

        // Variables dynamically assigned at the creation of a new GridItem.
        [SerializeField]
        private Image img;
        [SerializeField]
        private TMPro.TextMeshProUGUI desc;
        private ENTITY_TYPE type;

        // Method called by GridMenu when creating a new grid item. A Scriptable Object storing the target data of the object 
        // is passed as a method argument.
        public void Setup(GridItemSO _gridItemSO)
        {
            img.sprite = _gridItemSO.Sprite;
            desc.text = _gridItemSO.ItemName;
            type = _gridItemSO.Type;
        }

        // Trigger the event when the object is clicked. This method is handled by the EventTrigger component added in the Inspector.
        public void OnPointerClickedHandler()
        {
            OnGridItemSelectedEvDispatcher?.Invoke(type);
        }
    }
}