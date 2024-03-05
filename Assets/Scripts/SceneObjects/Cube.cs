using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class Cube : Entity
    {
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
            Debug.Log("[Cube] OnInitializedStateEnter");
        }

        private void OnDetachedStateEnter()
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("[Cube] OnDetachedStateEnter");
        }

        private void OnDestroyedStateEnter()
        {
            Destroy(gameObject);
        }
    }
}