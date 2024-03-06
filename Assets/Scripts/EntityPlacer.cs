using UnityEngine;
using UnityEngine.Events;

namespace markow
{
    public class EntityPlacer : MonoBehaviour
    {
        public class OnEntityDetachedEv : UnityEvent<GameObject> { }
        public OnEntityDetachedEv OnEntityDetachedEvDispatcher = new OnEntityDetachedEv();

        [SerializeField]
        private LayerMask ignoredLayerMask;
        private GameObject obj;
        private Camera mainCamera;
        private bool isEntityAttached = false;

        void Start()
        {
            mainCamera = Camera.main;
        }

        public void Init(GameObject _obj)
        {
            obj = _obj;

            if (!isEntityAttached) AttachCubeToCursor();
        }


        private void Update()
        {
            if (isEntityAttached && Input.GetMouseButtonDown(0))
            {
                DetachEntity();
            }


            if (isEntityAttached && obj != null)
            {
                MoveEntityWithCursor();
            }
        }

        private void AttachCubeToCursor()
        {
            if (obj != null)
            {
                isEntityAttached = true;
            }
        }

        private void MoveEntityWithCursor()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoredLayerMask))
            {
                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    obj.transform.position = hit.point + new Vector3(0, obj.transform.localScale.y / 2, 0);
                }
            }
        }

        private void DetachEntity()
        {
            OnEntityDetachedEvDispatcher?.Invoke(obj);

            isEntityAttached = false;
            obj = null;
        }

        public void Reset()
        {
            isEntityAttached = false;
            obj = null;
        }
    }
}