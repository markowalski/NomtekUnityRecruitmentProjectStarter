using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class Entity : MonoBehaviour
    {
        public ENTITY_TYPE type;
        protected ENTITY_STATE state = ENTITY_STATE.Disabled;

        public virtual void SetState(ENTITY_STATE _state)
        {

        }

        public ENTITY_STATE GetState()
        {
            return state;
        }
    }
}