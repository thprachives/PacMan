using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI redScoreText;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI greenScoreText;
    public TextMeshProUGUI yellowScoreText;

    private int redScore = 0;
    private int blueScore = 0;
    private int greenScore = 0;
    private int yellowScore = 0;

    // Singleton instance of ScoreManager
    public static ScoreManager instance;

    private void Awake()
    {
        // Ensure only one instance of ScoreManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementScore(Player currentPlayer)
{
    // Update the score based on the current player
    switch (currentPlayer)
    {
        case Player.RED:
            redScore++;
            redScoreText.text = redScore.ToString();
            break;
        case Player.BLUE:
            blueScore++;
            blueScoreText.text = blueScore.ToString();
            break;
        case Player.GREEN:
            greenScore++;
            greenScoreText.text = greenScore.ToString();
            break;
        case Player.YELLOW:
            yellowScore++;
            yellowScoreText.text = yellowScore.ToString();
            break;
    }
}

}
