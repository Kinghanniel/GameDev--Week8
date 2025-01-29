using UnityEngine;

namespace GameDevWithNey
{
    public class Aim : MonoBehaviour
    {

        public Camera playerCamera;  // Reference to the camera in the inspector
        public Transform GunsSpawnPos; // Spawn position for the guns
        public float zoomFOV = 30f;  // FOV when zoomed in
        public float defaultFOV = 60f;  // FOV when not zoomed
        public float zoomSpeed = 10f;  // How fast the zoom transitions


        //The new poasition and rotation when zoom in
        public Vector3 zoomedGunPosition;
        public Vector3 zoomedGunRotation;

        private Vector3 initialGunPosition;
        private Quaternion initialGunRotation;


        void Start()
        {
            // Set the camera to its default FOV at the start
            playerCamera.fieldOfView = defaultFOV;

            //store the initial position and rotation of the GunsSpawnPos
            initialGunPosition = GunsSpawnPos.localPosition;
            initialGunRotation = GunsSpawnPos.localRotation;
        }

        void Update()
        {
            // Check if the player is holding down the "T" key
            if (Input.GetKey(KeyCode.T))
            {
                // Smoothly transition to zoomed FOV
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomSpeed * Time.deltaTime);

                //,set the new position and rotation for the GunSpawnPos when zoom in
                GunsSpawnPos.localPosition = Vector3.Lerp(GunsSpawnPos.localPosition, zoomedGunPosition, zoomSpeed * Time.deltaTime);
                GunsSpawnPos.localRotation = Quaternion.Lerp(GunsSpawnPos.localRotation, Quaternion.Euler(zoomedGunRotation), zoomSpeed * Time.deltaTime);

            }
            else
            {
                // Smoothly transition back to default FOV
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV, zoomSpeed * Time.deltaTime);

                //Reset the position and rotation for the GunsSpawnPos
                GunsSpawnPos.localPosition = Vector3.Lerp(GunsSpawnPos.localPosition, initialGunPosition, zoomSpeed * Time.deltaTime);
                GunsSpawnPos.localRotation = Quaternion.Lerp(GunsSpawnPos.localRotation, initialGunRotation, zoomSpeed * Time.deltaTime);


            }
        }
    }
}
