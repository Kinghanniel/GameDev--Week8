using UnityEngine;

namespace GameDevWithMarco
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float interactRange = 2f; // Range within which the player can interact
        public KeyCode interactKey = KeyCode.R; // Key to interact
        public Camera playerCamera; // Reference to the player's camera

        private Car currentCar; // Reference to the car the player is in

        void Update()
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (currentCar != null && currentCar.IsPlayerInCar)
                {
                    // Exit the car
                    currentCar.Interact(gameObject);
                    currentCar = null; // Reset reference after exiting
                    return;
                }

                // Perform a raycast from the camera for interaction
                RaycastHit hit;
                Vector3 rayOrigin = playerCamera.transform.position;
                Vector3 rayDirection = playerCamera.transform.forward;

                Debug.DrawRay(rayOrigin, rayDirection * interactRange, Color.red, 1f); // Visualize the raycast

                if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactRange))
                {
                    Debug.Log($"Hit: {hit.collider.name}"); // Log the object hit

                    if (hit.collider.CompareTag("Car"))
                    {
                        Debug.Log("Hit a car!");

                        // Check for IInteractible component
                        IInteractible interactible = hit.collider.GetComponent<IInteractible>();
                        if (interactible != null)
                        {
                            interactible.Interact(gameObject);
                            if (interactible is Car car)
                            {
                                currentCar = car; // Store the reference to the car
                            }
                        }
                        else
                        {
                            Debug.LogError($"The car {hit.collider.name} does not have an IInteractible script.");
                        }
                    }
                    else
                    {
                        Debug.Log("Hit something that isn't a car.");
                    }
                }
                else
                {
                    Debug.Log("No object hit by raycast.");
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Draw the interaction range as a sphere in the Scene view
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}
