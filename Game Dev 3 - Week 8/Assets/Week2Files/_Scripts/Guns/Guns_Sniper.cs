using System.Collections;
using UnityEngine;

namespace GameDevWithNey.Guns
{
    public class Guns_Sniper : Guns_Parent
    {

        //override the MuzzleFlash method to add a delay
        public override void MuzzleFlash()
        {
            //start coroutine to add the delay
            StartCoroutine(MuzzleFlashWithDelay());
        }

        private IEnumerator MuzzleFlashWithDelay()
        {
            float delayTimme = 0.2f;
            yield return new WaitForSeconds(delayTimme);

            foreach (Transform barrel in tipOfTheBarrels)
            {
                // Spawn the muzzle flash after the delay
                GameObject flash = Instantiate(muzzleFlash, barrel.position, transform.rotation);
                flash.transform.Rotate(muzzleFlashRotationAdjustment);
            }
        }









        public override void GunSound()
        {
            gunAudioSource.pitch = Random.Range(0.6f, 1.4f);

            gunAudioSource.PlayOneShot(gunSound);
        }
    }
}
