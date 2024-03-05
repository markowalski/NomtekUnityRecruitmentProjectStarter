using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class CubeEater : Entity
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
            Debug.Log("[CubeEater] OnInitializedStateEnter");
        }

        private void OnDetachedStateEnter()
        {
            Debug.Log("[CubeEater] OnDetachedStateEnter");
        }

        private void OnDestroyedStateEnter()
        {
            Destroy(gameObject);
        }
    }
}