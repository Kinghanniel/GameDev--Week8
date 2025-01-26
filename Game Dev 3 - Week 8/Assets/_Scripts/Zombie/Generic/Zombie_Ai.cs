using UnityEngine;
using UnityEngine.AI;

namespace GameDevWithMarco.Enemy
{
    public class Zombie_Ai : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent agent;

        [SerializeField] public GameObject target;
        public float stoppingDistance = 1.1f;

        public bool isChasing = false;
        public bool isAttacking = false; // Add this flag to manage movement during attacks

        Zombie_Parent parentScript;
        Player.PlayerHealth playerHealth;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            HandlePlayerChange();

            

            if (target == null)
            {
                Debug.Log("Player target not found. Ensure the Player object has the 'Player' tag");
            }

            parentScript = GetComponent<Zombie_Parent>();
            agent.speed = parentScript.zombieSpeed;
        }

        void Update()
        {
            if (isAttacking)
            {
                StopMovement();
                return; // Prevent further updates while attacking
            }

            if (playerHealth != null && playerHealth.playerHealth <= 0)
            {
                isChasing = false;
                return;
            }

            if (isChasing)
            {
                ChasePlayer();
            }
            else
            {
                ResumeMovement();
            }
        }

        private void ChasePlayer()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance > stoppingDistance)
            {
                agent.destination = target.transform.position;
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
        }



        public void StopMovement()
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.isStopped = true;
                agent.destination = transform.position; // Freeze in place
            }
        }

        public void ResumeMovement()
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.isStopped = false;
            }
        }


        public void HandlePlayerChange()
        {
            target = GameObject.FindGameObjectWithTag("Player");

            if (target != null)
            {
                playerHealth = target.GetComponent<Player.PlayerHealth>();
                Debug.Log("Zombie target updated to new player.");
            }
            else
            {
                Debug.LogWarning("No Player object found after event.");
            }
        }
    }
}
