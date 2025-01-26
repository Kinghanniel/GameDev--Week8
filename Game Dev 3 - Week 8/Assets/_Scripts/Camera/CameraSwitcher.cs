using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco
{
    public class CameraSwitcher : MonoBehaviour
    {
        public Camera mainCamera;
        public Camera leftCam;
        public Camera rightCam;
        public Camera frontCam;

        private Camera[] cameras; // Array to hold all cameras
        private int currentIndex = 0; // Tracks the active camera index

        void Start()
        {
            // Initialize the cameras array
            cameras = new Camera[] { mainCamera, leftCam, rightCam, frontCam };

            // Ensure only the Main Camera is active at the start
            ActivateCamera(0);
        }

        void Update()
        {
            // Check if SPACE is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Increment the index and wrap around if necessary
                currentIndex = (currentIndex + 1) % cameras.Length;

                // Activate the camera at the new index
                ActivateCamera(currentIndex);
            }
        }

        private void ActivateCamera(int index)
        {
            // Deactivate all cameras
            foreach (Camera cam in cameras)
            {
                cam.gameObject.SetActive(false);
            }

            // Activate the selected camera
            cameras[index].gameObject.SetActive(true);
        }
    }
}
