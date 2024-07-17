using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollinDice : MonoBehaviour
{
    [SerializeField] Sprite[] numberSprite;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberGot;

    Coroutine generateRandomNomDice;
    public bool canDiceRoll = true;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void OnMouseDown()
    {
        generateRandomNomDice = StartCoroutine(RollDice());
    }

    public IEnumerator RollDice()
    {
        yield return new WaitForEndOfFrame();
        if (canDiceRoll)
        {
            canDiceRoll = false;
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.6f);

            numberGot = Random.Range(0, numberSprite.Length);
            numberSpriteHolder.sprite = numberSprite[numberGot];
            numberGot += 1;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            yield return new WaitForEndOfFrame();
            canDiceRoll = true;
            if (generateRandomNomDice != null)
            {
                StopCoroutine(RollDice());
            }

            int rollResult = numberGot; // Store the roll result
            gameManager.MovePiece(rollResult); // Pass the result to MovePiece
        }
    }
}
