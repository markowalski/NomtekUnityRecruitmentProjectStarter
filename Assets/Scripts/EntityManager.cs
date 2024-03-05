using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    [RequireComponent(typeof(EntityPlacer))]
    public class EntityManager : MonoBehaviour
    {
        [SerializeField]
        private Transform entityContainer;
        [SerializeField]
        private List<Entity> entities = new List<Entity>();
        private EntityPlacer entityPlacer;


        private void Awake()
        {
            entityPlacer = GetComponent<EntityPlacer>();

            GridMenu.OnGridMenuItemSelectedEvDispatcher.AddListener(OnGridMenuItemSelectedEvHandler);
            entityPlacer.OnEntityDetachedEvDispatcher.AddListener(OnEntityDetachedEvHandler);
        }

        private void OnEntityDetachedEvHandler(GameObject _obj)
        {
            
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
                    entityPlacer.Init(entityObj);

                    break;
                }

            }
        }
    }
}