using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static markow.GridItemListSO;

namespace markow
{
    public class GridMenu : MonoBehaviour
    {
        [SerializeField]
        private GridItemListSO gridItemListCFG;
        [SerializeField]
        private Transform itemContainer;
        [SerializeField]
        private GridItem gridItemPrefab;

        private void Start()
        {
            SetupGrid();
        }

        private void SetupGrid()
        {
            List<GridItemListField> list = gridItemListCFG.GetGridItemList();

            for (int i = 0; i < list.Count; i++)
            {
                GridItem gridItemObj = Instantiate(gridItemPrefab, itemContainer);
                gridItemObj.name = list[i].title;
                gridItemObj.Setup(list[i].content);
            }
        }
    }
}