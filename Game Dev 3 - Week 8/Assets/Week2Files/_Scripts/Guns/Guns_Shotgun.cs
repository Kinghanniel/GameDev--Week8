using UnityEngine;

namespace GameDevWithNey.Guns
{
    public class Guns_Shotgun : Guns_Parent
    {
        public override void GunSound()
        {
            //randomises the pitch of the sound between two values
            gunAudioSource.pitch = Random.Range(0.6f, 1.4f);

            //plays the sound once
            gunAudioSource.PlayOneShot(gunSound);
        }

        public override void MuzzleFlash()
        {
            //will execute the muzzleflash
            base.MuzzleFlash();

            //stores the muzzle flash particle system
            ParticleSystem.MainModule muzzleFlashParticles = muzzleFlash.GetComponent<ParticleSystem>().main;
            
            //change the color of the muzzle flash
            muzzleFlashParticles.startColor = Color.white;

        }

    }
}
