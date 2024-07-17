using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pellet : MonoBehaviour
{
    private ScoreManager scoreManager; // Reference to the ScoreManager instance

    void Start()
    {
        // Find the ScoreManager instance in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager instance not found in the scene!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            // Get the color of the player
            //Color playerColor = other.GetComponent<SpriteRenderer>().color;

            // Call the IncrementScore method of the ScoreManager with the player's color
            
            if (scoreManager != null)
            {
            Player currentPlayer = GameManager.instance.returncurrentplayer();
            scoreManager.IncrementScore(currentPlayer);
            }

            else
            {
                Debug.LogError("ScoreManager instance is null!");
            }

            gameObject.SetActive(false);
            Invoke("Respawn", 1f);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }
}