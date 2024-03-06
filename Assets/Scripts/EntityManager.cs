using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace markow
{
    /*
     * Class responsible for creating and changing states of 3D objects.
     * The class utilizes another required component, EntityPlacer, to which it passes the created objects for moving them around the scene.
     */

    // Required component for the class to function
    [RequireComponent(typeof(EntityPlacer))]
    public class EntityManager : MonoBehaviour
    {
        // Event declaration broadcasted when a 3D object is dropped onto the scene
        public class OnEntityPlacedEv : UnityEvent { }

        public OnEntityPlacedEv OnEntityPlacedEvDispatcher = new OnEntityPlacedEv();

        // Reference to the container for created objects (to maintain order).
        // SerializeField attribute allows for access to the property despite its private access modifier
        [SerializeField]
        private Transform entityContainer;

        // Reference to all types of objects that can be created.
        [SerializeField]
        private List<Entity> entities = new List<Entity>();
        private EntityPlacer entityPlacer;

        // Variable storing the currently created object
        private Entity currentEntity;

        private void Awake()
        {
            entityPlacer = GetComponent<EntityPlacer>();

            // Listen if EntityPlacer has released the object and stopped working
            entityPlacer.OnEntityDetachedEvDispatcher.AddListener(OnEntityDetachedEvHandler);
        }

        // If EntityPlacer finished working on an object, change the object's state to Detached, and then send information about the EntityManager's completion of work
        private void OnEntityDetachedEvHandler(GameObject _obj)
        {
            _obj.GetComponent<Entity>().SetState(ENTITY_STATE.Detached);
            OnEntityPlacedEvDispatcher?.Invoke();
            currentEntity = null;
        }

        // Create a 3D object based on a prefab of a specific type
        public void DeployEntity(ENTITY_TYPE _type)
        {
            // Search the object collection and if one has the requested type, create its clone
            foreach (var entity in entities)
            {
                if (entity.EntityType == _type)
                {
                    GameObject entityObj = Instantiate(entity.gameObject, entityContainer);
                    currentEntity = entityObj.GetComponent<Entity>();

                    // Change the state of the created object
                    currentEntity.SetState(ENTITY_STATE.Initialized);

                    // Pass the created object to EntityPlacer so it can place it in the scene
                    entityPlacer.Init(entityObj);
                    break;
                }
            }
        }

        // Immediately interrupt the action of setting the current object and change its state to Destroyed
        public void UndeployEntity()
        {
            if (currentEntity != null) currentEntity.SetState(ENTITY_STATE.Destroyed);
            entityPlacer.Reset();
        }
    }
}
