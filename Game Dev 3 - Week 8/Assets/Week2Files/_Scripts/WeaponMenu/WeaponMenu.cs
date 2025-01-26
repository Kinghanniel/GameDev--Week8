using UnityEngine;

namespace GameDevWithNey
{
    public class WeaponMenu : MonoBehaviour
    {
        //Reference to the Menu tab
        public GameObject weaponMenu;

        //Boolean to track if the UI is active
        private bool isUIActive = false;
       

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Toggle the active state of the UI
                isUIActive = !isUIActive;
                weaponMenu.SetActive(isUIActive);

                //Toggle the cursor visibility and lock state
                if (isUIActive)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None; 
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked; 

                }
            }
        }
    }
}
