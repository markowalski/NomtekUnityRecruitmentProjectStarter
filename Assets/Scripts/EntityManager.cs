using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace markow
{
    [RequireComponent(typeof(EntityPlacer))]
    public class EntityManager : MonoBehaviour
    {
        public class OnEntityPlaceddEv : UnityEvent { }
        public OnEntityPlaceddEv OnEntityPlaceddEvDispatcher = new OnEntityPlaceddEv();

        [SerializeField]
        private Transform entityContainer;
        [SerializeField]
        private List<Entity> entities = new List<Entity>();
        private EntityPlacer entityPlacer;


        private void Awake()
        {
            entityPlacer = GetComponent<EntityPlacer>();
            entityPlacer.OnEntityDetachedEvDispatcher.AddListener(OnEntityDetachedEvHandler);
        }

        private void OnEntityDetachedEvHandler(GameObject _obj)
        {
            _obj.GetComponent<Entity>().SetState(ENTITY_STATE.Detached);
            OnEntityPlaceddEvDispatcher?.Invoke();
        }

        private void OnGridMenuItemSelectedEvHandler(ENTITY_TYPE _type)
        {
            SetupEntity(_type);
        }

        public void SetupEntity(ENTITY_TYPE _type)
        {
            foreach (var entity in entities)
            {
                if (entity.EntityType == _type)
                {
                    GameObject entityObj = Instantiate(entity.gameObject, entityContainer);
                    entityObj.GetComponent<Entity>().SetState(ENTITY_STATE.Initialized);
                    entityPlacer.Init(entityObj);

                    break;
                }

            }
        }
    }
}