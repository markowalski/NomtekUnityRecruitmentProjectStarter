using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            state = _state;

            switch (state)
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
            Debug.Log("[CubeEater] OnInitializedStateEnter");
        }

        private void OnDetachedStateEnter()
        {
            FindClosestTarget();
            Debug.Log("[CubeEater] OnDetachedStateEnter");
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
                if (targetEntity && targetEntity.GetState() != ENTITY_STATE.Detached) continue;

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
                Debug.Log("[CubeEater] FindClosestTarget " + target.name, target);
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
            if (state == ENTITY_STATE.Detached && target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (state == ENTITY_STATE.Detached && collision.gameObject.CompareTag(targetTag))
            {
                collision.gameObject.GetComponent<Entity>().SetState(ENTITY_STATE.Destroyed);
                FindClosestTarget();
            }
        }
    }
}