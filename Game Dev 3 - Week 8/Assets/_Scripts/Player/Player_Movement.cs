using UnityEngine;


namespace GameDevWithMarco.Player
{
    public class Player_Movement : MonoBehaviour
    {
        /// <summary>
        /// Car controls borrowed and adapted from:
        /// https://www.youtube.com/watch?v=cqATTzJmFDY
        /// 
        /// More interesting concepts in this video if you want to expand:
        /// https://www.youtube.com/watch?v=CBgtU9FCEh8
        /// The controls need to be tweaked and expanded to dit your needs!
        /// </summary>

        //Reference to the sphere's rigidbody
        public Rigidbody sphereRb;

        //Car driving variables
        public float forwardAccell = 8f;
        public float reverseAccel = 4f;
        public float maxSpeed = 50f;
        public float turnSrenght = 180f;
        public float gravityForce = 10f;
        private float dragOnValue = 3f;


        //Grounded Variables
        private bool grounded;
        public LayerMask whatIsGround;
        public float groundRayLenght = 0.5f;
        public Transform groundRayPoint;


        //Inputs for controls
        float speedInput;
        float turnInput;

        //Wheels variables
        public Transform leftFrontWheel, rightFrontWheel;
        public float maxWheelTurn = 25f;

        // Start is called before the first frame update
        void Start()
        {
            sphereRb.transform.parent = null;
        }

        // Update is called once per frame
        void Update()
        {
            AccellerationDecelleration();

            if (grounded)
            {
                Turning();
            }

            WheelsRotation();

            SetPositionToBeSameAsSphere();
        }



        private void FixedUpdate()
        {
            GroundCheck();


            if (grounded)
            {
                AccelleratioForce();
            }
            else
            {
                PushCarDownToGround();
            }
        }

        private void PushCarDownToGround()
        {
            sphereRb.drag = 0.1f;       //Reduces the drag thus making us continue to move in the air

            sphereRb.AddForce(Vector3.up * -gravityForce * 1000);
        }

        private void GroundCheck()
        {
            grounded = false;
            RaycastHit hit;

            if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLenght, whatIsGround))
            {
                grounded = true;


                //Adjusts the car rotation to match the slope we are going up and down to
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }

        private void WheelsRotation()
        {
            leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
            rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn), rightFrontWheel.localRotation.eulerAngles.z);
        }

        private void AccelleratioForce()
        {
            sphereRb.drag = dragOnValue;    //Restores the drag to the original value

            if (Mathf.Abs(speedInput) > 0)
            {
                sphereRb.AddForce(transform.forward * speedInput);
            }
        }


        private void SetPositionToBeSameAsSphere()
        {
            transform.position = sphereRb.transform.position;
        }

        private void Turning()
        {
            turnInput = Input.GetAxis("Horizontal");

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSrenght * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        private void AccellerationDecelleration()
        {
            speedInput = 0;

            if (Input.GetAxis("Vertical") > 0)
            {
                speedInput = Input.GetAxis("Vertical") * forwardAccell * 1000;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000;
            }
        }
    }
}
