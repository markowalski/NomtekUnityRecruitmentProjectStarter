using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static markow.GridItemListSO;
using DG.Tweening;

namespace markow
{
    /*
    *  Class responsible for managing the entire GridMenu used for selecting elements to be placed on the scene and for filtering their content.
    */

    public class GridMenu : MonoBehaviour
    {
        //  event dispatched at the moment of selecting an item in the menu
        public class OnGridMenuItemSelectedEv : UnityEvent<ENTITY_TYPE> { }
        public OnGridMenuItemSelectedEv OnGridMenuItemSelectedEvDispatcher = new OnGridMenuItemSelectedEv();

        //  previously created list of objects based on which, the menu items will be created
        [SerializeField]
        private GridItemListSO gridItemListCFG;

        //  container to which the created menu items will be attached
        [SerializeField]
        private Transform itemContainer;

        //  prefab based on which copies constituting the menu items will be created
        [SerializeField]
        private GridItem gridItemPrefab;

        //  list of all created menu items
        private List<GridItem> instantiatedItems = new List<GridItem>();
        private RectTransform rTransform;
        private float xPos;

        private void Awake()
        {
            //  get the reference to RectTransform
            rTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            //  fill the menu at the start of the application
            SetupGrid();
        }

        //  method for showing the menu
        public void Show()
        {
            //  animate xPos variable to the value of 0 over half a second, apply animation smoothing
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

        //  main method for filling the GridMenu
        private void SetupGrid()
        {
            //  get the list of objects created in the editor
            List<GridItemListField> list = gridItemListCFG.GridItemList;

            //  iterate over the list of objects
            for (int i = 0; i < list.Count; i++)
            {
                //  create an object corresponding to each item on the list
                GridItem gridItemObj = Instantiate(gridItemPrefab, itemContainer);
                gridItemObj.name = list[i].title;

                //  setup the created object by injecting data from ScriptableObject
                gridItemObj.Setup(list[i].content);

                //  add to each item in the list a listener, in case it is clicked by the player
                gridItemObj.OnGridItemSelectedEvDispatcher.AddListener(OnGridItemClickedEvHandler);

                //  add the item to the list of all created items
                instantiatedItems.Add(gridItemObj);
            }
        }

        //  method called in response to an item being clicked in the menu
        private void OnGridItemClickedEvHandler(ENTITY_TYPE _type)
        {
            //  if an item is clicked, dispatch another event informing other components that GridMenu is ready for the next operation
            OnGridMenuItemSelectedEvDispatcher?.Invoke(_type);

            //  hide the menu
            Hide();
        }

        //  method for filtering objects in the GridMenu. It is called by the EventTrigger part of TextMeshPRO, every time there is a change in the text.
        public void FilterGridMenu(string _str)
        {
            //  iterate over the list of all created objects
            for (int i = 0; i < instantiatedItems.Count; i++)
            {
                //  if the object's name contains the searched string, enable the element
                if (instantiatedItems[i].name.IndexOf(_str, StringComparison.OrdinalIgnoreCase) >= 0)
                    instantiatedItems[i].gameObject.SetActive(true);
                else
                    instantiatedItems[i].gameObject.SetActive(false);
            }
        }
    }
}
