using System.Collections;
using UnityEngine;

namespace markow
{
    /*
    *  Stores the logic for CubeEater object. A subclass of the Entity class.
    *  In the project, we differentiate two types of 3D objects: Cube and CubeEater. Both classes have a similar structure,
    *  where a key element is the Switch statement that, depending on the set state of the object, triggers the corresponding method.
    */

    public class CubeEater : Entity
    {
        // Tag of objects that Eater should seek.
        [SerializeField]
        private string targetTag;
        // Movement speed of the Eater.
        [SerializeField]
        private float speed;
        // Rotation speed of the Eater.
        [SerializeField]
        private float angularSpeed;

        // Variable storing information about the Eater's closest target.
        private Transform target;

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
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void OnDetachedStateEnter()
        {
            // Invoke a Coroutine that cyclically checks if the CubeEater's targets exist on the scene
            StartCoroutine(TryFindClosestTarget());
        }

        // For optimization reasons, there's no need to call the method every frame in Update, a Coroutine is better suited for this
        private IEnumerator TryFindClosestTarget()
        {
            // As long as true is true (which means always), perform the loop
            while (true)
            {
                FindClosestTarget();

                yield return new WaitForSeconds(.25f);
            }
        }

        private void OnDestroyedStateEnter()
        {
            Destroy(gameObject);
        }

        // Method called every time CubeEater is released or has just destroyed another target.
        private void FindClosestTarget()
        {
            if (target) return;

            // Find all objects in the scene with the tag.
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

            // Set the minimum distance to the maximum value.
            float minDistance = Mathf.Infinity;

            GameObject closest = null;
            Entity targetEntity = null;

            // Iterate over all objects with the tag.
            foreach (GameObject potentialTarget in targets)
            {
                // Get the Entity component from each object and check if the target is indeed released on the Floor, if not, skip to the next object.
                targetEntity = potentialTarget.GetComponent<Entity>();
                if (targetEntity && targetEntity.EntityState != ENTITY_STATE.Detached) continue;

                // Get the distance to the object.
                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);

                // If the distance is smaller than minDistance, overwrite it. This way, the shortest distance is always recorded.
                if (distance < minDistance)
                {
                    closest = potentialTarget;
                    minDistance = distance;
                }
            }

            // If the closest object was found and exists, assign it as the target.
            if (closest != null)
            {
                target = closest.transform;
            }
            else
            {
                target = null;
            }
        }

        private void Update()
        {
            // Invoke the move method toward the closest target each frame. This is necessary to achieve smooth movement based on Time.deltaTime.
            MoveTowardClosestTarget();
        }

        private void MoveTowardClosestTarget()
        {
            // If CubeEater is released and has a target.
            if (entityState == ENTITY_STATE.Detached && target != null)
            {
                // Determine the direction vector based on the positions of CubeEater and its target.
                Vector3 direction = (target.position - transform.position).normalized;
                // Move CubeEater in the specified direction.
                transform.position += direction * speed * Time.deltaTime;

                // Determine the looking direction of CubeEater based on the previously described direction.
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                // Start rotating the object.
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
            }
        }

        // Method invoked when CubeEater collides with another object.
        void OnCollisionEnter(Collision collision)
        {
            // If the collision is with an object with the tag.
            if (entityState == ENTITY_STATE.Detached && collision.gameObject.CompareTag(targetTag))
            {
                // Retrieve information from the object and set its state to Destroyed.
                collision.gameObject.GetComponent<Entity>().SetState(ENTITY_STATE.Destroyed);

                //  Then search for the next target. 
                //  I left the invocation of this method to allow CubeEater to move smoothly from object to object, not waiting for the Coroutine call
                FindClosestTarget();
            }
        }
    }
}
