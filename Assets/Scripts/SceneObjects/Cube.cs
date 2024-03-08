using System;
using System.Collections;
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
        //  Set the current state of the object
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
                case ENTITY_STATE.Undetected:
                    OnUndetectedStateEnter();
                    break;
                case ENTITY_STATE.Destroyed:
                    OnDestroyedStateEnter();
                    break;
            }
        }

        private void OnInitializedStateEnter()
        {
            //  Disable physics on objects that are being moved but have not yet been dropped
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void OnDetachedStateEnter()
        {
            //  Enable physics on objects that have been dropped
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        //  Call the method at the moment the object loses contact with the floor
        private void OnUndetectedStateEnter()
        {
            //  change the state of the falling object to Destroyed with a time delay
            StartCoroutine(SetDestroyedStateWithDelay());
        }

        //  Method to prevent objects from falling indefinitely
        private IEnumerator SetDestroyedStateWithDelay()
        {
            yield return new WaitForSeconds(2f);

            SetState(ENTITY_STATE.Destroyed);
        }

        private void OnDestroyedStateEnter()
        {
            //  Destroy the object in the Destroyed state
            Destroy(gameObject);
        }

        //  Call the method when the Cube stops colliding with an object
        private void OnCollisionExit(Collision collision)
        {
            //  If that object is Floor, change the state to Undetected
            if (collision.gameObject.tag == Tags.Floor)
            {
                SetState(ENTITY_STATE.Undetected);
            }
        }
    }
}
