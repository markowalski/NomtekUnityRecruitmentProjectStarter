using UnityEngine;

namespace markow
{
    /*
    *  Stores the logic for Cube-type objects. A subclass of the Entity class.
    *  In the project, we differentiate two types of 3D objects: Cube and CubeEater. Both classes have a similar structure,
    *  where a key element is the Switch statement that, depending on the set state of the object, triggers the corresponding method.
    */
    public class Cube : Entity
    {
        //  set the current state of the object
        public override void SetState(ENTITY_STATE _state)
        {
            entityState = _state;

            switch (entityState)
            {
                case ENTITY_STATE.Disabled:
                    break;
                case ENTITY_STATE.Initialized:
                    OnInitializedStateEnter();
                    break;
                case ENTITY_STATE.Detached:
                    OnDetachedStateEnter();
                    break;
                case ENTITY_STATE.Destroyed:
                    OnDestroyedStateEnter();
                    break;
            }
        }

        private void OnInitializedStateEnter()
        {
            //  disable physics on objects that are being moved but have not yet been dropped
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void OnDetachedStateEnter()
        {
            //  disable physics on objects that have been dropped
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void OnDestroyedStateEnter()
        {
            //  destroy the object in the Destroyed state
            Destroy(gameObject);
        }
    }
}
