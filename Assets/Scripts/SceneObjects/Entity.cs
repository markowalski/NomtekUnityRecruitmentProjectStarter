using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        private ENTITY_TYPE entityType;
        protected ENTITY_STATE entityState = ENTITY_STATE.Disabled;

        public virtual void SetState(ENTITY_STATE _state) { }


        public ENTITY_TYPE EntityType 
        {
            get => entityType;
            private set => entityType = value;
        }

        public ENTITY_STATE EntityState 
        { 
            get => entityState;
            protected set => entityState = value;
        }
    }
}