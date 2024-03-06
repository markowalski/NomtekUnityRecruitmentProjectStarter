using UnityEngine;

namespace markow
{
    /*
     * The main class managing the scene. The only class that contains direct references to the main components.
     * To maintain encapsulation, all components in the project are responsible only for themselves,
     * but there are situations where they need to communicate with each other without adding direct references.
     * SceneLogic receives information from key components and communicates with them.
     * The two main components in the project are EntityManager responsible for managing 3D objects on the scene
     * and GridMenu responsible for the operation of the 2D menu.
    */

    public class SceneLogic : MonoBehaviour
    {
        // reference to EntityManager
        [SerializeField]
        private EntityManager entityManager;
        // reference to GridMenu
        [SerializeField]
        private GridMenu gridMenu;

        private void Awake()
        {
            // listen for an event sent when a 3D object has been dropped onto the scene
            entityManager.OnEntityPlacedEvDispatcher.AddListener(OnEntityPlaceddEvHandler);
            // listen for an event sent when any menu item has been selected
            gridMenu.OnGridMenuItemSelectedEvDispatcher.AddListener(OnGridMenuItemSelectedEvHandler);
        }

        // if a 2D element from the menu has been selected, take its type and run the function to create a 3D object in EntityManager
        private void OnGridMenuItemSelectedEvHandler(ENTITY_TYPE _type)
        {
            entityManager.DeployEntity(_type);
        }

        // if a 3D object has been placed on the scene, show the menu again
        private void OnEntityPlaceddEvHandler()
        {
            gridMenu.Show();
        }

        // if the ESCAPE button has been pressed, destroy the held 3D object and show the menu
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
