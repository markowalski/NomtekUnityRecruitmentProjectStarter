using System;
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
        private List<GridItem> instantiatedItems = new List<GridItem>();

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
                gridItemObj.OnGridItemSelectedEvDispatcher.AddListener(OnGridItemClickedEvHandler);

                instantiatedItems.Add(gridItemObj);
            }
        }

        private void OnGridItemClickedEvHandler(ENTITY_TYPE _type)
        {
            Debug.Log("[GridMenu] OnGridItemClickedEvHandler " + _type.ToString());
        }

        public void FilterGridMenu(string _str)
        {
            for (int i = 0; i < instantiatedItems.Count; i++)
            {
                if (instantiatedItems[i].name.IndexOf(_str, StringComparison.OrdinalIgnoreCase) >= 0)
                    instantiatedItems[i].gameObject.SetActive(true);
                else instantiatedItems[i].gameObject.SetActive(false);
            }
        }
    }
}