using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class EntityManager : MonoBehaviour
    {
        [SerializeField]
        private Transform entityContainer;

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
            foreach (var entity in entities)
            {
                if (entity.type == _type)
                {
                    GameObject entityObj = Instantiate(entity.gameObject, entityContainer);
                    break;
                }

            }
        }
    }
}