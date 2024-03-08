using UnityEngine;
using UnityEngine.Events;

namespace markow
{
    /*
     * Class responsible for placing 3D objects on the scene. The class takes a created 3D object
     * as the main argument and allows moving it across the plane using the mouse cursor.
    */

    public class EntityPlacer : MonoBehaviour
    {

        // declaration of an event that informs other components that the object has been successfully placed
        public class OnEntityDetachedEv : UnityEvent<GameObject> { }
        public OnEntityDetachedEv OnEntityDetachedEvDispatcher = new OnEntityDetachedEv();

        // reference to the mask that our raycast targeting the floor will ignore
        [SerializeField]
        private LayerMask ignoredLayerMask;

        // variable in which we will store the object during its placement
        private GameObject obj;
        private Camera mainCamera;

        // whether the object is currently being moved
        private bool isEntityAttached = false;

        void Start()
        {
            mainCamera = Camera.main;
        }

        // method accepting the created object
        public void Init(GameObject _obj)
        {
            obj = _obj;

            if (!isEntityAttached) AttachCubeToCursor();
        }


        private void Update()
        {
            // if the object is held and the mouse button is pressed, invoke the "drop" method
            if (isEntityAttached && Input.GetMouseButtonDown(0))
            {
                DetachEntity();
            }

            // if the object is held and the mouse button is not pressed, move the object
            if (isEntityAttached && obj != null)
            {
                MoveEntityWithCursor();
            }
        }

        // if the object exists, attach it to the cursor
        private void AttachCubeToCursor()
        {
            if (obj != null)
            {
                isEntityAttached = true;
            }
        }

        // method for moving the object attached to the cursor
        private void MoveEntityWithCursor()
        {
            // fire a raycast from the camera in the direction of the cursor
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // let the raycast ignore objects on the ignoredLayerMask layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoredLayerMask))
            {
                // execute if the raycast hits an object with the "Floor" Tag
                if (hit.collider.gameObject.CompareTag(Tags.Floor))
                {
                    // to the hit position returned by hit, we add half the height of the object so it doesn't sink into the floor
                    obj.transform.position = hit.point + new Vector3(0, obj.transform.localScale.y / 2, 0);
                }
            }
        }

        // detach the object from the cursor
        private void DetachEntity()
        {
            // inform other listening components about this
            OnEntityDetachedEvDispatcher?.Invoke(obj);
            Reset();
        }

        // stop/reset the work of EntityPlacer
        public void Reset()
        {
            isEntityAttached = false;
            obj = null;
        }
    }
}
