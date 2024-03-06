using UnityEngine;

namespace markow
{
    /*
    *  Similar to GridItemListSO, a class that serves as a template for creating ScriptableObjects for each type of object appearing in the GridMenu list
    *  All variables, although available from the Unity Inspector, are hidden with an access modifiers for other components.
    */

    [CreateAssetMenu(fileName = "GridItem", menuName = "GridMenu/GridItem")]
    public class GridItemSO : ScriptableObject
    {
        [SerializeField]
        private string itemName;
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private ENTITY_TYPE type;

        public string ItemName
        {
            get => itemName;
            private set => itemName = value;
        }

        public Sprite Sprite
        {
            get => sprite;
            private set => sprite = value;
        }

        public ENTITY_TYPE Type
        {
            get => type;
            private set => type = value;
        }
    }
}