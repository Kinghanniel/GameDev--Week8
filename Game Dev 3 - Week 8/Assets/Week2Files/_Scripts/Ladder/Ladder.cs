using UnityEngine;

namespace GameDevWithNey
{
    public class Ladder : MonoBehaviour
    {

        public float climbSpeed = 5; // climbing speed
        private bool isClimbing; //flag if the player is climbing

        private CharacterController controller;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();

        }

        // Update is called once per frame
        void Update()
        {
            
            if (isClimbing)
            {
                // Move the player upwards when they press "W" or the forward input
                if (Input.GetKey(KeyCode.W))
                {
                   Vector3 climbMovement = new Vector3(0, climbSpeed * Time.deltaTime, 0);
                  controller.Move(climbMovement);

                }
            }
            
        }

        // Detect when the player enters the ladder area
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ladder"))
            {
                isClimbing = true;
            }
        }
        
        // Detect when the player exits the ladder area
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ladder"))
            {
                isClimbing = false;
            }
        }
    }
}
