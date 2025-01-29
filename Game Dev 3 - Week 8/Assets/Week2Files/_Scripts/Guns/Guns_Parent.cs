using DG.Tweening;
using GameDevWithNey.ScriptabelObjects;
using UnityEngine;

namespace GameDevWithNey.Guns
{

    public abstract class Guns_Parent : MonoBehaviour
    {
        /// <summary>
        /// The purpose of this script is to create a "Parent" class
        /// that we can then use in "children" classes to make them inherits 
        /// variables and methods from it.
        /// </summary>

        //KEYWORDS:
        //public            -- WILL be inherited and WILL be visible in the editor
        //private           -- WILL NOT be inherited and WILL NOT be visible in the editor
        //protected         -- WILL inherited but WILL NOT be visible in the editor
        //abstract class    -- means that this is a polymorphism class
        //virtual           -- means that that method can be overridden (modified in a child script)


        //These three variables will deal with the muzzle flash
        public Transform[] tipOfTheBarrels;
        public Transform bulletShell;
        public GameObject muzzleFlash;
        public Vector3 muzzleFlashRotationAdjustment = new Vector3(0, 95, 0);
        public Vector3 bulletRotationAdjustment = new Vector3(0, 95, 0);


        public GameObject bulletPrefab;
        public GameObject bulletShellPrefab;
        public float bulletSpeed = 20f;

        // Fire rate variables
        public float fireRate = 0.5f;  // Time between shots
        private float nextFireTime = 0f;  // Tracks when the gun can fire next
        public bool gunAuto; //Control the gun fire rate

        //Recoil variables
        protected Vector3 originalPosition;
        public Vector3 recoilAmount;
        public float recoilDuration;

        //Gun sound variables
        public AudioClip gunSound;
        protected AudioSource gunAudioSource;

        // Reference to the latest spawned gun
        private GameObject latestSpawnedGun;
        public GunsType_ScriptableObject gunsScriptable;


        public virtual void Start()
        {
            originalPosition = transform.position;
            gunAudioSource = GetComponent<AudioSource>();


        }

        protected virtual void Update()
        {
            // Auto-fire: Keep firing while holding the right mouse button
            if (gunsScriptable.gunAuto)
            {
                // Check if the gun is allowed to fire based on the cooldown
                if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
                {
                    nextFireTime = Time.time + gunsScriptable.fireRate;  // Set the next time the gun can fire
                    FireBullet();
                    MuzzleFlash();
                    GunSound();
                    Recoil();
                }
            }
            else
            {
                // Single fire: Fire once when pressing right mouse button
                if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime)
                {
                    nextFireTime = Time.time + gunsScriptable.fireRate;
                    FireBullet();
                    MuzzleFlash();
                    GunSound();
                    Recoil();
                }
            }
        }

        public virtual void MuzzleFlash()
        {
            foreach (Transform barrel in tipOfTheBarrels)
            {
                //THe first line will spawn the muzzle flash
                GameObject flash = Instantiate(muzzleFlash, barrel.position, transform.rotation);
                //The next line will adjust the rotation of it
                flash.transform.Rotate(muzzleFlashRotationAdjustment);
            }

        }

        protected virtual void FireBullet()
        {
            foreach (Transform barrel in tipOfTheBarrels)
            {
                //Instantiate the bullet at the tip of the barrel
                GameObject bullet = Instantiate(bulletPrefab, barrel.position, transform.rotation);
                GameObject shell = Instantiate(bulletShellPrefab, bulletShell.position, transform.rotation);

                // Adjust the rotation of the bullet based on the bulletRotationAdjustment
                bullet.transform.Rotate(bulletRotationAdjustment);

                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                if (bullet != null)
                {
                    //set bullet velocity 
                    bulletRb.velocity = bullet.transform.forward * gunsScriptable.bulletSpeed; // Use the adjusted forward direction
                }

                // Destroy the bullet and bulletShell after 3 seconds 
                //Destroy(bullet, 0.5f);
                Destroy(shell, 3f);
                Debug.Log("Shots fired");
            }
        }

        public virtual void Reload()
        {
            Debug.Log("Reloading Parent");
        }


        public virtual void Recoil()
        {

            // Store the gun's initial local rotation (relative to its parent)
            Quaternion originalRotation = latestSpawnedGun.transform.localRotation;

            // Kill any existing tweens to prevent conflicting rotations
            latestSpawnedGun.transform.DOKill();

            // Immediately reset the gun's rotation before applying recoil
            latestSpawnedGun.transform.localRotation = originalRotation;

            // Apply the punch rotation for recoil, using local rotation
            latestSpawnedGun.transform.DOPunchRotation(gunsScriptable.recoilAmount, gunsScriptable.recoilDuration)
            .OnComplete(() =>
            {
                // After the punch effect, smoothly return the gun to its original local rotation
                latestSpawnedGun.transform.DOLocalRotateQuaternion(originalRotation, 0.2f);
            });

        }
        // Method to set the latest spawned gun reference
        public void SetLatestSpawnedGun(GameObject gun)
        {
            latestSpawnedGun = gun;
        }



        //Will force each child to implement their version of the GunSound() method
        public abstract void GunSound();

    }


}



