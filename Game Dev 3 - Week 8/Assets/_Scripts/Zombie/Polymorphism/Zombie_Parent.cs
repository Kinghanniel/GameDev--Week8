using UnityEngine;

namespace GameDevWithMarco.Enemy
{
    public class Zombie_Parent : MonoBehaviour
    {

        public ZombieType whatTypeIsThisZombie;     //Will help us define what type this zombie is

        public float zombieSpeed;                   //So we can specify its speed

        public enum ZombieType
        {
            Walker,
            Runner
        }
    }
}
