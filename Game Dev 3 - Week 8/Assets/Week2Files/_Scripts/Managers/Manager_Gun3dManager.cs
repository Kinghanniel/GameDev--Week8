using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithNey.ScriptabelObjects;
using GameDevWithNey.Guns;


namespace GameDevWithNey.Managers
{
    public class Manager_Gun3dManager : MonoBehaviour
    {
        //Variable to set WHERE the gun will spawn
        public Transform gunSpawnPos;

        // Variable to keep track of the spawned gun
        GameObject latestSpawnedGun;

        

        public void SpawnGun(GunsType_ScriptableObject activeSO)
        {
            //Will destroy the last gun that was spawned if there is any
            if (latestSpawnedGun != null)
            {
                Destroy(latestSpawnedGun);
            }

            //latestSpawnedGun Will store the newly spawned gun, so if we spawn another one we can destroy it
            //Instantiate will spawn the gun. Instantiate(what, where, what rotation).
            //With Quaternion.Euler() is can specify a specific Vector3 for the rotation
            latestSpawnedGun = Instantiate(activeSO.gunPrefab, gunSpawnPos.position, Quaternion.Euler(0, 0, 0));

            // Set the player camera or another relevant GameObject as the parent to make the gun move with the player
            latestSpawnedGun.transform.SetParent(gunSpawnPos);
            latestSpawnedGun.transform.localPosition = Vector3.zero;  // Reset position relative to parent
            latestSpawnedGun.transform.localRotation = Quaternion.Euler(0, 0, 0);  // Reset rotation relative to parent

            // Set the reference of the spawned gun to the Guns_Parent script
            Guns_Parent gunScript = latestSpawnedGun.GetComponent<Guns_Parent>();
            if (gunScript != null)
            {
                gunScript.SetLatestSpawnedGun(latestSpawnedGun);  // Pass the gun to Guns_Parent
            }
        }
    }

}
