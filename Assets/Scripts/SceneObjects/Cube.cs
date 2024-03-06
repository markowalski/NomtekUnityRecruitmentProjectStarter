using UnityEngine;

namespace markow
{
    public class Cube : Entity
    {
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
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void OnDestroyedStateEnter()
        {
            Destroy(gameObject);
        }
    }
}