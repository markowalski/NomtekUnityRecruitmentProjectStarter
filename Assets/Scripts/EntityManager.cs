using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class EntityManager : MonoBehaviour
    {
        [SerializeField]
        private List<Entity> entities = new List<Entity>();

        private void Awake()
        {
            GridMenu.OnGridMenuItemSelectedEvDispatcher.AddListener(OnGridMenuItemSelectedEvHandler);
        }

        private void OnGridMenuItemSelectedEvHandler(ENTITY_TYPE _type)
        {
            SetupEntity(_type);
        }

        public void SetupEntity(ENTITY_TYPE _type)
        {

        }
    }
}