using System;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    /*
     * The class serves as a template for creating lists of objects occurring in GridMenu.
     * Thanks to the use of ScriptableObject, the list is editable and operates independently of the loaded scene.
    */

    // Attribute that enables easy creation of objects based on the template in the Unity editor
    [CreateAssetMenu(fileName = "GridItemList", menuName = "GridMenu/GridItemList")]
    public class GridItemListSO : ScriptableObject
    {
        // A serializable type created specifically for the Unity inspector. With this approach, the list will be readable and will contain both title and content as a single field.
        // Struct, as opposed to classes, is a simplified type, ideally suited for such solutions.
        [Serializable]
        public struct GridItemListField
        {
            public string title;

            // each list element contains a previously prepared ScriptableObject with the configuration of the given element
            public GridItemSO content;
        }

        // the main list exposed in the Unity Inspector
        [SerializeField]
        private List<GridItemListField> gridItemList;

        public List<GridItemListField> GridItemList
        {
            get => gridItemList;
            private set => gridItemList = value;
        }
    }
}
