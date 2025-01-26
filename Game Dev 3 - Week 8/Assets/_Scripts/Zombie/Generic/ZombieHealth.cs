using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco
{
    public class ZombieHealth : MonoBehaviour
    {
        public float zombieHealth = 5;

        public void TakeDamage(float damageAmount)
        {
            zombieHealth -= damageAmount;

            if (zombieHealth <= 0)
            {
                Debug.Log("Zombie is dead");
                Destroy(gameObject);
            }
        }
    }
}
