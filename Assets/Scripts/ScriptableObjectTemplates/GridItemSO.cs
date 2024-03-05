using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace markow
{
    [CreateAssetMenu(fileName = "GridItem", menuName = "GridMenu/GridItem")]
    public class GridItemSO : ScriptableObject
    {
        public new string name;
        public Sprite sprite;
        public ENTITY_TYPE type;
    }
}