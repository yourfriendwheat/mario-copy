using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] [Range(0f, 1f)] float Coin = .5f;

    bool wasCollected = false;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only process if the player collides and the coin hasn't been collected yet
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;  // Mark the coin as collected
            Debug.Log("Coin was collected: " + wasCollected); // Confirm this is true when the player collides

            // Add score
            var gameSession = Object.FindFirstObjectByType<GameSession>();
            if (gameSession != null)
            {
                PlayClip(coinPickupSFX, Coin);
            }
            else
            {
                Debug.LogError("GameSession object not found!");
            }

            
            Debug.Log("Destroying coin...");
  
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(100);
            PlayClip(coinPickupSFX, Coin);
            Destroy(gameObject);  // Destroy the coin once collected

        }
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }



}




