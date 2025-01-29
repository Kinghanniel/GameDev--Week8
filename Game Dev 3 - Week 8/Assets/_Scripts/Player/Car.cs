using GameDevWithMarco.ObserverPattern;
using UnityEngine;

namespace GameDevWithMarco
{
    public class Car : MonoBehaviour, IInteractible
    {
        public Transform playerNewPos;
        private bool playerInCar = false; // Tracks whether the player is in the car
        private GameObject currentPlayer; // Reference to the player GameObject
        public GameEvent enteredCar;
        public GameEvent exitCar;

        public bool IsPlayerInCar => playerInCar; // Public property to check car state

        public void Interact(GameObject player)
        {
            Debug.Log($"{player.name} interacted with the car!");
            if (playerInCar)
            {
                // Exit the car
                player.transform.SetParent(null); // Detach the player from the car
                player.transform.position = transform.position + transform.right * 2; // Place player outside the car
                currentPlayer = null;
                playerInCar = false;
                exitCar.Raise();
                Debug.Log("Player exited the car.");
            }
            else
            {
                // Enter the car
                player.transform.position = playerNewPos.transform.position; // Move player to car's position
                player.transform.SetParent(playerNewPos); // Make player a child of the car
                currentPlayer = player;
                playerInCar = true;
                enteredCar.Raise();
                Debug.Log("Player entered the car.");
            }
        }
    }
}
