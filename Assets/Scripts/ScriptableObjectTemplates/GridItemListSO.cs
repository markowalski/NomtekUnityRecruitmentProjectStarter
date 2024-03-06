using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    [CreateAssetMenu(fileName = "GridItemList", menuName = "GridMenu/GridItemList")]
    public class GridItemListSO : ScriptableObject
    {
        [Serializable]
        public struct GridItemListField
        {
            public string title;
            public GridItemSO content;
        }

        [SerializeField]
        private List<GridItemListField> gridItemList;

        public List<GridItemListField> GridItemList
        {
            get => gridItemList;
            private set => gridItemList = value;
        }
    }
}