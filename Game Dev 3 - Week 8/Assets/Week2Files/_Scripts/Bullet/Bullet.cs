using GameDevWithMarco;
using UnityEngine;

namespace GameDevWithNey
{
    public class Bullet : MonoBehaviour
    {
        public float damageAmount = 1;

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Zombie"))
            {
                ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();

                if (zombieHealth != null)
                {
                    zombieHealth.TakeDamage(damageAmount);

                    Debug.Log("Zombie lost healht" + damageAmount);
                }
            }


        }





    }
}
