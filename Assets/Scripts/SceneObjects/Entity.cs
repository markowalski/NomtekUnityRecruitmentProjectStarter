using UnityEngine;

namespace markow
{
    /*
    *  Class attached to all 3D objects present in the scene (except the Floor). The Entity class serves as a parent for two other classes: Cube and CubeEater.
    *  To avoid creating exceptions when distinguishing objects into Cube and CubeEater types, these inherit from Entity.
    */
    public class Entity : MonoBehaviour
    {
        //  each entity has its own type, which is set once on the object in the inspector
        [SerializeField]
        private ENTITY_TYPE entityType;
        
        //  each entity has its own state, the change of which is protected and only possible from a child class level
        protected ENTITY_STATE entityState = ENTITY_STATE.Disabled;

        //  a public virtual method for setting states, which will be inherited by children of the Entity class
        //  the method is empty because, depending on the child type, it will be filled with different content
        public virtual void SetState(ENTITY_STATE _state) { }

        //  public properties to read the current type and state of the object
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
