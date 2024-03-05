using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class EntityPlacer : MonoBehaviour
    {
        private GameObject obj;
        private Camera mainCamera;
        private bool isCubeAttached = false;

        void Start()
        {
            mainCamera = Camera.main;
        }

        public void Init(GameObject _obj)
        {
            obj = _obj;

            if (!isCubeAttached) AttachCubeToCursor();
        }


        private void Update()
        {
            if (isCubeAttached && Input.GetMouseButtonDown(0))
            {
                DetachCube();
            }


            if (isCubeAttached && obj != null)
            {
                MoveCubeWithCursor();
            }
        }

        private void AttachCubeToCursor()
        {
            if (obj != null)
            {
                isCubeAttached = true;
            }
        }

        private void MoveCubeWithCursor()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    obj.transform.position = hit.point + new Vector3(0, obj.transform.localScale.y / 2, 0);
                }
            }
        }

        private void DetachCube()
        {
            isCubeAttached = false;
            obj = null;
        }
    }
}