using UnityEngine;

namespace GameDevWithMarco.Enemy
{
    public class scp_Zombie_EyeSight : MonoBehaviour
    {
        Zombie_Ai ai;

        private void Start()
        {
            ai = GetComponentInParent<Zombie_Ai>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ai.isChasing = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ai.isChasing = false;
            }
        }

    }
}
