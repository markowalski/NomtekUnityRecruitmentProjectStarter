using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static markow.GridItemListSO;
using DG.Tweening;

namespace markow
{
    public class GridMenu : MonoBehaviour
    {
        public class OnGridMenuItemSelectedEv : UnityEvent<ENTITY_TYPE> { }
        public OnGridMenuItemSelectedEv OnGridMenuItemSelectedEvDispatcher = new OnGridMenuItemSelectedEv();

        [SerializeField]
        private GridItemListSO gridItemListCFG;
        [SerializeField]
        private Transform itemContainer;
        [SerializeField]
        private GridItem gridItemPrefab;

        private List<GridItem> instantiatedItems = new List<GridItem>();
        private RectTransform rTransform;
        private float xPos;

        private void Awake()
        {
            rTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            SetupGrid();
        }

        public void Show()
        {
            DOTween.To(() => xPos, x => xPos = x, 0f, .5f).SetEase(Ease.OutCubic)
            .OnUpdate(() => {
                rTransform.anchoredPosition = new Vector2(xPos, rTransform.anchoredPosition.y);
            });
        }

        public void Hide()
        {
            DOTween.To(() => xPos, x => xPos = x, -rTransform.rect.width, .5f).SetEase(Ease.InCubic)
            .OnUpdate(() => {
                rTransform.anchoredPosition = new Vector2(xPos, rTransform.anchoredPosition.y);
            });
        }

        private void SetupGrid()
        {
            List<GridItemListField> list = gridItemListCFG.GridItemList;

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
            OnGridMenuItemSelectedEvDispatcher?.Invoke(_type);
            Hide();
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