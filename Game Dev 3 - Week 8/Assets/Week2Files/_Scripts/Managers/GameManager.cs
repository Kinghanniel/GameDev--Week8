using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameDevWithNey
{
    public class GameManager : MonoBehaviour
    {
        public float countdownTime = 90f; // Set your countdown time
        public Text timerText; // UI Text to display the timer
        public GameObject loseMenu; // Reference to the lose menu UI
        public GameObject winMenu; // Reference to the win menu UI

        private void Start()
        {
            Time.timeScale = 1;  // Reset time scale when the scene starts
            loseMenu.SetActive(false); // Hide lose menu at start
            winMenu.SetActive(false); // Hide win menu at start
            StartCoroutine(Countdown());

            Cursor.visible = false; // Hide the cursor
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        }

        private IEnumerator Countdown()
        {
            while (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
                timerText.text = Mathf.Ceil(countdownTime).ToString(); // Update the timer UI
                yield return null; // Wait for the next frame
            }

            // When the timer reaches zero, check for vases
            CheckVases();
        }

        private void Update()
        {
            CheckVases();

        }

        private void CheckVases()
        {
            // Check if there are any objects with the tag "Vase"
            GameObject[] vases = GameObject.FindGameObjectsWithTag("Vase");

            if (vases.Length > 0 && countdownTime <= 0)
            {
                // If there are vases and the timer is at zero, show the lose menu
                loseMenu.SetActive(true);
                Time.timeScale = 0; // Pause the game

                Cursor.visible = true; // Make the cursor visible
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            }
            else if (vases.Length == 0)
            {
                // If there are no vases, show the win menu
                winMenu.SetActive(true);
                Time.timeScale = 0;

                Cursor.visible = true; // Make the cursor visible
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            }
        }

        public void ReplayGame()
        {
            Debug.Log("Game should replay");
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene

        }
    }
}
