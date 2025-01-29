using GameDevWithMarco.ObserverPattern;
using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health and VFX")]
        public float playerHealth;
        public GameObject hitVFX;
        public GameObject damageSmokeVFX;
        public GameObject deathVFX;
        public GameObject deathFireSmokeVFX;
        public GameObject deathExplosionVFX;
        public GameObject deathBoomTextVFX;

        [Header("Instantiate Current Car Prefab")]

        public GameEvent playerDeath;
        public GameObject carPrefab;



        [Header("Transform Properties")]
        public Transform deathPos;
        public Transform deathTextPos;
        public Transform smokePOS;
        public Transform explosionPOS;


        [Header("Speed Properties")]
        public float speedAffectedByDamage = 2f;


        private Player_Movement playerMovement;
        private GameObject activeSmokeVFX; // Reference to the instantiated smoke VFX
        bool isSmokeVFXInstantiated = false; // Tracks if the smoke VFX has been instantiated
        bool isdeathVFXInstantiated = false; // Tracks if the death VFX has been instantiated


        private void Start()
        {
            playerMovement = GetComponent<Player_Movement>();
        }


        void Update()
        {
            //PlayDamagedSmokeVFX();
        }


        public void TakeDamage(float damage, Vector3 hitPosition)
        {
            playerHealth -= damage;


            // Stop damage smoke if health reaches 0
            if (playerHealth <= 0)
            {
                StopDamagedSmokeVFX();
            }
            else
            {
                PlayDamagedSmokeVFX();
            }

            Death();


            if (hitVFX != null)
            {
                // Instantiate VFX at the collision position
                GameObject hitEffects = Instantiate(hitVFX, hitPosition, hitVFX.transform.rotation);
            }
        }

        //instantiated VFX and make sure the player dies
        void Death()
        {
            if (playerHealth <= 0 && !isdeathVFXInstantiated)
            {
                playerHealth = 0;

                if (deathVFX != null)
                {
                    GameObject deathEffect = Instantiate(deathVFX, deathPos.position, deathVFX.transform.rotation);
                    GameObject fireSmokeEffect = Instantiate(deathFireSmokeVFX, smokePOS.position, deathFireSmokeVFX.transform.rotation);
                    GameObject explosionEffect = Instantiate(deathExplosionVFX, explosionPOS.position, deathExplosionVFX.transform.rotation);
                    GameObject textEffect = Instantiate(deathBoomTextVFX, deathTextPos.position, deathBoomTextVFX.transform.rotation);


                    //set the instantiated Objects to a new parent
                    textEffect.transform.parent = deathPos.transform.parent;
                    deathEffect.transform.parent = deathTextPos.transform.parent;
                    fireSmokeEffect.transform.parent = smokePOS.transform.parent;
                    explosionEffect.transform.parent = explosionPOS.transform.parent;

                    isdeathVFXInstantiated = true;
                    InstantiateCarPrefab();
                }

                playerDeath?.Raise();



            }
        }

        void PlayDamagedSmokeVFX()
        {
            if (playerHealth > 1 && playerHealth <= 50 && !isSmokeVFXInstantiated)
            {
                if (damageSmokeVFX != null)
                {
                    activeSmokeVFX = Instantiate(damageSmokeVFX, smokePOS.position, damageSmokeVFX.transform.rotation);
                    GameObject explosionVFX = Instantiate(deathExplosionVFX, explosionPOS.position, deathExplosionVFX.transform.rotation);



                    explosionVFX.transform.parent = explosionPOS.transform.parent;
                    activeSmokeVFX.transform.parent = smokePOS.transform.parent;
                    isSmokeVFXInstantiated = true;

                    // Modify player movement speed
                    playerMovement.forwardAccell = speedAffectedByDamage;
                }
            }

            //controlls player movements

            else if (playerHealth > 50)
            {
                // Reset player speed if health is above 50
                playerMovement.forwardAccell = 6f;
            }
        }

        void StopDamagedSmokeVFX()
        {
            if (activeSmokeVFX != null)
            {
                ParticleSystem smokeParticleSystem = activeSmokeVFX.GetComponent<ParticleSystem>();
                if (smokeParticleSystem != null)
                {
                    // Stop and clear the particle system
                    smokeParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    smokeParticleSystem.Clear(); // Clears any lingering particles
                }

                isSmokeVFXInstantiated = false; // Reset the flag
            }
        }

        public void InstantiateCarPrefab()
        {
            GameObject destroyedCar = Instantiate(carPrefab, transform.position, transform.rotation);
            destroyedCar.transform.parent = null;
        }

    }
}
