
using UnityEngine;
using GameDevWithMarco.Player;
namespace GameDevWithMarco
{
    public class Damage : MonoBehaviour
    {
        public float damageAmount;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    // Calculate the approximate collision position
                    Vector3 collisionPoint = other.ClosestPoint(transform.position);

                    // Pass both damage and collision point
                    playerHealth.TakeDamage(damageAmount, collisionPoint);
                    Debug.Log("Player took damage at position: " + collisionPoint);
                }
            }
           // else
           // {
             //   Destroy(gameObject);
           // }
        }
    }
}
