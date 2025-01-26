using UnityEngine;
using UnityEngine.AI;
using GameDevWithMarco.Enemy;

namespace GameDevWithMarco.StatePattern
{
    public class Zombie_StateMachine_Moving : Zombie_StateMachine_BaseState
    {
        private Vector3 randomDestination;
        private float destinationThreshold = 1.5f; // Distance to consider a destination "reached"
        private bool isWaiting = false; // Flag to prevent multiple triggers

        public override void EnterState(Zombie_StateMachine_Manager stateMachineManager)
        {
            if (stateMachineManager.isPlayerDead) return;

            // Initialize NavMeshAgent
            var agent = stateMachineManager.zombieAiScript.agent;
            if (agent == null || !agent.isOnNavMesh)
            {
                Debug.LogWarning("Zombie is not on the NavMesh!");
                return;
            }

            // Set an initial random destination
            SetRandomDestination(stateMachineManager);

            // Play the correct animation
            PlayMovementAnimation(stateMachineManager);
        }

        public override void UpdateState(Zombie_StateMachine_Manager stateMachineManager)
        {
            var agent = stateMachineManager.zombieAiScript.agent;

            if (isWaiting) return; // Skip updates if waiting

            if (!agent.pathPending && agent.remainingDistance <= destinationThreshold)
            {
                stateMachineManager.StartCoroutine(WaitAndSetNextDestination(stateMachineManager));
            }
        }

        private System.Collections.IEnumerator WaitAndSetNextDestination(Zombie_StateMachine_Manager stateMachineManager)
        {
            isWaiting = true;
            var agent = stateMachineManager.zombieAiScript.agent;

            // Stop agent and play idle animation
            agent.isStopped = true;
            stateMachineManager.animScript.PlayIdleAnimation();

            // Wait for 3 seconds
            yield return new WaitForSeconds(3f);

            // Resume movement
            agent.isStopped = false;
            SetRandomDestination(stateMachineManager);

            isWaiting = false;
        }

        private void SetRandomDestination(Zombie_StateMachine_Manager stateMachineManager)
        {
            var agent = stateMachineManager.zombieAiScript.agent;
            if (agent == null || !agent.isOnNavMesh) return;

            Vector3 randomPoint = stateMachineManager.transform.position + Random.insideUnitSphere * 10f; // Radius of 10 units
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                randomDestination = hit.position;
                agent.SetDestination(randomDestination);
                PlayMovementAnimation(stateMachineManager); // Ensure animation matches state
            }
        }

        private void PlayMovementAnimation(Zombie_StateMachine_Manager stateMachineManager)
        {
            switch (stateMachineManager.zombieParent.whatTypeIsThisZombie)
            {
                case Zombie_Parent.ZombieType.Walker:
                    stateMachineManager.animScript.PlayWalkAnimation();
                    break;
                case Zombie_Parent.ZombieType.Runner:
                    stateMachineManager.animScript.PlayRunAnimation();
                    break;
                default:
                    Debug.LogWarning("Unknown zombie type: No animation played.");
                    break;
            }
        }

        public override void OnCollisionEnter(Zombie_StateMachine_Manager stateMachineManager, Collision collision)
        {
            if (!stateMachineManager.zombieAiScript.isAttacking)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    stateMachineManager.SwitchState(stateMachineManager.attackState);
                }
                else if (collision.gameObject.CompareTag("Car"))
                {
                    stateMachineManager.zombieAiScript.target = collision.gameObject; // Set the car as the target
                    stateMachineManager.SwitchState(stateMachineManager.attackState);
                }
            }

        }

        public override void OnTriggerEnter(Zombie_StateMachine_Manager stateMachineManager, Collider collider)
        {
            if (!stateMachineManager.zombieAiScript.isAttacking)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    stateMachineManager.SwitchState(stateMachineManager.attackState);
                }
                else if (collider.gameObject.CompareTag("Car"))
                {
                    stateMachineManager.zombieAiScript.target = collider.gameObject; // Set the car as the target
                    stateMachineManager.SwitchState(stateMachineManager.attackState);
                }
            }

        }



        public override void OnCollisionExit(Zombie_StateMachine_Manager stateMachineManager, Collision collision) { }
    }
}
