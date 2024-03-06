using UnityEngine;

namespace markow
{
    public class CubeEater : Entity
    {
        [SerializeField]
        private string targetTag;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float angularSpeed;

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
            FindClosestTarget();
        }

        private void OnDestroyedStateEnter()
        {
            Destroy(gameObject);
        }

        private void FindClosestTarget()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            float minDistance = Mathf.Infinity;
            GameObject closest = null;
            Entity targetEntity = null;

            foreach (GameObject potentialTarget in targets)
            {
                targetEntity = potentialTarget.GetComponent<Entity>();
                if (targetEntity && targetEntity.EntityState != ENTITY_STATE.Detached) continue;

                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < minDistance)
                {
                    closest = potentialTarget;
                    minDistance = distance;
                }
            }

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
            MoveTowardClosestTarget();
        }

        private void MoveTowardClosestTarget()
        {
            if (entityState == ENTITY_STATE.Detached && target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (entityState == ENTITY_STATE.Detached && collision.gameObject.CompareTag(targetTag))
            {
                collision.gameObject.GetComponent<Entity>().SetState(ENTITY_STATE.Destroyed);
                FindClosestTarget();
            }
        }
    }
}