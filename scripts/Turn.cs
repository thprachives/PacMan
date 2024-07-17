using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    Text mytext;
    SpriteRenderer squareOutlineRenderer; // Reference to the square outline's SpriteRenderer

    // Start is called before the first frame update
    void Start()
    {
        mytext = GetComponent<Text>();
        mytext.text = "Game Start";
        GameManager.instance.message += UpdateMessage;

        // Get the SpriteRenderer of the square outline
        squareOutlineRenderer = GameManager.instance.squareOutline.GetComponent<SpriteRenderer>();
    }

    void UpdateMessage(Player player)
    {
        mytext.text = GameManager.instance.hasGameFinished ? "GAME OVER" : player.ToString() + "'S TURN";

        // Update square outline color based on current player
        switch (player)
        {
            case Player.RED:
                squareOutlineRenderer.color = Color.red;
                break;
            case Player.BLUE:
                squareOutlineRenderer.color = Color.blue;
                break;
            case Player.GREEN:
                squareOutlineRenderer.color = Color.green;
                break;
            case Player.YELLOW:
                squareOutlineRenderer.color = Color.yellow;
                break;
        }
    }
}
