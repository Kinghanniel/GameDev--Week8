using UnityEngine;

namespace GameDevWithMarco.StatePattern
{
    public abstract class Zombie_StateMachine_BaseState
    {

        /// <summary>
        /// This script defines the base of what the states will be.
        /// It has four methods that will be used to achieve different results.
        /// </summary>


        public abstract void EnterState(Zombie_StateMachine_Manager stateMachineManager);

        public abstract void UpdateState(Zombie_StateMachine_Manager stateMachineManager);

        public abstract void OnCollisionEnter(Zombie_StateMachine_Manager stateMachineManager, Collision collision);

        public abstract void OnTriggerEnter(Zombie_StateMachine_Manager stateMachineManager, Collider collider);
        public abstract void OnCollisionExit(Zombie_StateMachine_Manager stateMachineManager, Collision collision);
    }
}
