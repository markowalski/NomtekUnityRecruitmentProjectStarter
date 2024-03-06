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
        }

        private void OnEntityPlaceddEvHandler()
        {
            gridMenu.Show();
        }
    }
}