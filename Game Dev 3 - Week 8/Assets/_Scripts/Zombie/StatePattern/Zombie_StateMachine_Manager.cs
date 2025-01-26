using UnityEngine;
using GameDevWithMarco.Enemy;
using Unity.VisualScripting;
using GameDevWithMarco.Player;
using GameDevWithMarco.ObserverPattern;
using System.Collections.Generic;

namespace GameDevWithMarco.StatePattern
{
    public class Zombie_StateMachine_Manager : MonoBehaviour
    {
        /// <summary>
        /// This script will manage the transitions from state to state
        /// of the state machine.
        /// </summary>


        public float damageAmount = 1f;
        [SerializeField] Zombie_StateMachine_BaseState currentState;     //This variable will keep track of what state is currently active

        //These variables will store references to the script that manage each state 
        public Zombie_StateMachine_IdleState idleState = new Zombie_StateMachine_IdleState();
        public Zombie_StateMachine_Moving movingState = new Zombie_StateMachine_Moving();
        public Zombie_StateMachine_AttackState attackState = new Zombie_StateMachine_AttackState();


        //Variables to manage animations
        public Zombie_Animations animScript;

        //Variable to check the zombie type
        public Zombie_Parent zombieParent;

        //Zombie ai/navigation script
        public Zombie_Ai zombieAiScript;

        public bool isPlayerDead = false;
        // Subscribe to the GameEvent by adding a listener to the response

        private Rigidbody zombieRigidbody; // Reference to the Rigidbody component




        // Start is called before the first frame update
        void Start()
        {
            animScript = GetComponent<Zombie_Animations>();         //Finds the Animator        
            zombieAiScript = GetComponent<Zombie_Ai>();             //Will find the ai script        
            zombieParent = GetComponent<Zombie_Parent>();           //Will find the parent script

            currentState = movingState; // default state
            currentState?.EnterState(this); // execute the EnterState() when Start() would happen

            zombieRigidbody = GetComponent<Rigidbody>(); 

        }


        private void Update()
        {
            currentState?.UpdateState(this); //execute the UpdateState() when Update() happens
        }

        private void OnCollisionEnter(Collision collision)
        {
            currentState?.OnCollisionEnter(this, collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            currentState?.OnTriggerEnter(this, other);

            

        }
        
        private void OnCollisionExit(Collision collision)
        {
            currentState?.OnCollisionExit(this, collision);
        }


        public void SwitchState(Zombie_StateMachine_BaseState stateWeWantToUse)
        {
            if (zombieAiScript.agent != null && zombieAiScript.agent.isOnNavMesh)
            {
                currentState = stateWeWantToUse;
                currentState?.EnterState(this);
            }
        }


        public void OnPlayerDeath()
        {
            isPlayerDead = true;
            zombieAiScript.isAttacking = false;
            zombieAiScript.isChasing = false;

            // Ensure the zombie resumes random movement
            if (currentState != movingState)
            {
                SwitchState(movingState);
            }



        }

        public bool IsPlayerDead()
        {
            return isPlayerDead;
        }
    }
}
