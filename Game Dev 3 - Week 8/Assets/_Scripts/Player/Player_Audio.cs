using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class Player_Audio : MonoBehaviour
    {
        public AudioClip gargle;
        AudioSource audioSource;


        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }


        public void PlayGargleSound()
        {
            float randomPitch = Random.Range(0.5f, 1.5f);
            audioSource.pitch = randomPitch;
            audioSource.PlayOneShot(gargle);
        }
    }
}
