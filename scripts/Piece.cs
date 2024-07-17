using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [SerializeField]
    List<Color> colors;
    [SerializeField] AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    bool canMove;
    bool dead;
    int moveIndex;
    List<int> movePos;
    float speed = 1f;
    GameManager manager = GameManager.instance;
    int[] dir = { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };

    private void Awake(){
        //movement = GetComponent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        moveIndex = 1;
        GetComponent<AnimatedSprite>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("start");
        if (!canMove) {
            return;
        }    
        GetComponent<AnimatedSprite>().enabled = true;
        float step = speed * Time.deltaTime;
        Vector3 targetPos = GameManager.instance.positions[movePos[moveIndex]];
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        if(Vector3.Distance(transform.position,targetPos) < 0.001f)
        {
            ChangeDirection();
            moveIndex++;
            if(moveIndex==movePos.Count-1 && dead){
                DeathSequence();
                Invoke(nameof(ResetState), 3f);
                dead = false;
            }
            if(moveIndex == movePos.Count)
            {
                moveIndex = 1;
                canMove = false;
                manager.nextTurn = true;
            }
        }
    }
    // Method to change direction based on completion of row
    void ChangeDirection()
    {
        int currRow = (movePos[moveIndex]) / 10;
        int prevRow = (movePos[moveIndex - 1]) / 10;
        // If they belong to different rows, change direction
        if (currRow != prevRow)
        {
            // Determine current direction based on the dir array
            Vector2 currentDirection = dir[currRow] == 0 ? Vector2.right : Vector2.left;
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }
    }

    public void SetColors(Player player)
    {
        spriteRenderer.color = colors[(int)player];
        deathSequence.SetColors(colors[(int)player]);

    }

    public void SetMovement(List<int> result,bool died)
    {
        movePos = result;
        canMove = true;
        dead = died;
        if(result.Count==2 && dead){
            DeathSequence();
            Invoke(nameof(ResetState), 3f);
            dead = false;
        }
    }
    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        deathSequence.enabled = false;
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }
    
}