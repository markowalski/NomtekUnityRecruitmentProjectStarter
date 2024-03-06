using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace markow
{
    public class SceneLogic : MonoBehaviour
    {
        [SerializeField]
        private EntityManager entityManager;
        [SerializeField]
        private GridMenu gridMenu;

        private void Awake()
        {
            entityManager.OnEntityPlaceddEvDispatcher.AddListener(OnEntityPlaceddEvHandler);
            gridMenu.OnGridMenuItemSelectedEvDispatcher.AddListener(OnGridMenuItemSelectedEvHandler);
        }

        private void OnGridMenuItemSelectedEvHandler(ENTITY_TYPE _type)
        {
            entityManager.DeployEntity(_type);
        }

        private void OnEntityPlaceddEvHandler()
        {
            gridMenu.Show();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape)) 
            {
                gridMenu.Show();
                entityManager.UndeployEntity();
            }
        }
    }
}