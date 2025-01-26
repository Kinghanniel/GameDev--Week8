using GameDevWithMarco.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Player;
using GameDevWithMarco.Enemy;


namespace GameDevWithMarco
{
    public class Zombie_StateMachine_AttackState : Zombie_StateMachine_BaseState
    {
        Zombie_Ai zombieAI;
        Transform playerTransform;
        private float exitAttackDistance = 1.5f; // Allowable distance before switching states
        private float attackCooldown = 2f; // Time between attacks
        private float lastAttackTime;
        public override void EnterState(Zombie_StateMachine_Manager stateMachineManager)
        {

            zombieAI = stateMachineManager.zombieAiScript;  // Initialize zombieAI

        }

        public override void UpdateState(Zombie_StateMachine_Manager stateMachineManager)
        {
            if (zombieAI.target == null)
            {
                zombieAI.isAttacking = false;
                stateMachineManager.SwitchState(stateMachineManager.movingState);
                return;
            }

            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(zombieAI.transform.position, zombieAI.target.transform.position);


            // Attack logic
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                zombieAI.isAttacking = true;
                zombieAI.StopMovement(); // Halt movement
                stateMachineManager.animScript.PlayAttackAnimation();

                // Damage logic
                if (zombieAI.target.CompareTag("Player"))
                {
                    // Deal damage to the player
                    Debug.Log("Zombie attacks player!");
                }
                else if (zombieAI.target.CompareTag("Car"))
                {
                    // Deal damage to the car
                    Debug.Log("Zombie attacks the car!");
                   
                }
            }

            // Exit attack state if target is out of range
            if (distanceToTarget > zombieAI.stoppingDistance + exitAttackDistance)
            {
                zombieAI.isAttacking = false;
                stateMachineManager.SwitchState(stateMachineManager.movingState);
            }
        }

        public override void OnCollisionEnter(Zombie_StateMachine_Manager stateMachineManager, Collision collision)
        {
            zombieAI = stateMachineManager.zombieAiScript;
            playerTransform = stateMachineManager.zombieAiScript.target?.transform;


            // Stop movement and attack
            zombieAI.StopMovement();
            stateMachineManager.animScript.PlayAttackAnimation();
        }

        public override void OnTriggerEnter(Zombie_StateMachine_Manager stateMachineManager, Collider collider)
        {

        }

        public override void OnCollisionExit(Zombie_StateMachine_Manager stateMachineManager, Collision collision)
        {
            // Stop attacking when the player leaves the zombie's collider
            if (collision.gameObject.CompareTag("Player"))
            {
                zombieAI.isAttacking = false;
                stateMachineManager.SwitchState(stateMachineManager.movingState);
            }
        }




    }
}
