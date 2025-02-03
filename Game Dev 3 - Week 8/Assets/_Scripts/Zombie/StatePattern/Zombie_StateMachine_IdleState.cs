using UnityEngine;

namespace GameDevWithMarco.StatePattern
{
    // This class represents the "Idle" state for a zombie in a state machine pattern.
    // It inherits from Zombie_StateMachine_BaseState, which is likely an abstract class defining the common interface for all zombie states.
    public class Zombie_StateMachine_IdleState : Zombie_StateMachine_BaseState
    {
        // This method is called when the zombie enters the "Idle" state.
        public override void EnterState(Zombie_StateMachine_Manager stateMachineManager)
        {
            // Play the idle animation for the zombie.
            stateMachineManager.animScript.PlayIdleAnimation();
        }

        // This method is called every frame while the zombie is in the "Idle" state.
        public override void UpdateState(Zombie_StateMachine_Manager stateMachineManager)
        {
            // Check if the zombie is in a chasing state.
            if (stateMachineManager.zombieAiScript.isChasing == true)
            {
                // If the zombie is chasing, switch to the "Moving" state.
                stateMachineManager.SwitchState(stateMachineManager.movingState);
            }
        }

        // This method is called when the zombie collides with another object.
        public override void OnCollisionEnter(Zombie_StateMachine_Manager stateMachineManager, Collision collision)
        {
            // Check if the collided object has the "Player" tag.
            if (collision.gameObject.tag == "Player")
            {
                // If the collided object is the player, switch to the "Attack" state.
                stateMachineManager.SwitchState(stateMachineManager.attackState);
            }
        }

        // This method is called when a trigger collider is entered.
        // Currently, it does nothing, but it can be used to handle trigger-based events in the future.
        public override void OnTriggerEnter(Zombie_StateMachine_Manager stateMachineManager, Collider collider)
        {

        }

        // This method is called when a collision with another object ends.
        // Currently, it does nothing, but it can be used to handle post-collision logic in the future.
        public override void OnCollisionExit(Zombie_StateMachine_Manager stateMachineManager, Collision collision)
        {

        }
    }
}