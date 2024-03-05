using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    [CreateAssetMenu(fileName = "GridItemList", menuName = "GridMenu/GridItemList")]
    public class GridItemListSO : ScriptableObject
    {
        [SerializeField]
        private List<GridItemListField> gridItemList;

        [Serializable]
        public struct GridItemListField
        {
            public string title;
            public GridItemSO content;
        }

        public List<GridItemListField> GetGridItemList()
        {
            return gridItemList;
        }
    }
}