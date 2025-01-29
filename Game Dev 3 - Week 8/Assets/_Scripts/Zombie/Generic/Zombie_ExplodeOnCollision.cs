using GameDevWithMarco.Player;
using UnityEngine;

namespace GameDevWithMarco.Enemy
{
    public class Zombie_ExplodeOnCollision : MonoBehaviour
    {
        /// <summary>
        /// Used this video to create the green explosion particle system: https://www.youtube.com/watch?v=lw4T8gfcKZ0
        /// </summary>


        //Particles
        public GameObject greenExplosion;
        public GameObject purpleExplosion;

        //COnnections to other scripts
        private Player_Audio playerAudioScript;
        private Zombie_Parent zombieParent;

        private void Start()
        {
            playerAudioScript = FindObjectOfType<Player_Audio>();
            zombieParent = GetComponent<Zombie_Parent>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Car")
            {
                Rigidbody rb = collision.gameObject.GetComponent<Player_Movement>().sphereRb;
                //code blocker
                if (rb.velocity.x < 1 && rb.velocity.z < 1) return;

                //Checks the type of the zombie
                switch (zombieParent.whatTypeIsThisZombie)
                {
                    case Zombie_Parent.ZombieType.Walker:
                        //To spawn the particle system
                        var walkerParticles = Instantiate(greenExplosion, collision.transform.position, Quaternion.identity);
                        //To destroy the particle system
                        Destroy(walkerParticles, 2f);
                        break;
                    case Zombie_Parent.ZombieType.Runner:
                        //To spawn the particle system
                        var runnerParticles = Instantiate(purpleExplosion, collision.transform.position, Quaternion.identity);
                        //To destroy the particle system
                        Destroy(runnerParticles, 2f);
                        break;
                    default:
                        break;
                }

                //Plays the gargle sound
                playerAudioScript.PlayGargleSound();
                //To destroy the gameObject this script is attached to
                Destroy(gameObject);
            }
        }
    }
}
