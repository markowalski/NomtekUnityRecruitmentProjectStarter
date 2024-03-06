using UnityEngine;

namespace markow
{
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