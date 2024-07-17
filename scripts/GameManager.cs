using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public bool hasGameFinished, canClick;

    public static GameManager instance;
    public bool nextTurn;
    public GameObject spinningWheelGameObject;
    public GameObject gameDice;
     public GameObject squareOutline; 

    int roll;

    [SerializeField]
    GameObject gamePiece;

    [SerializeField]
    Vector3 startPos;

    public Board myboard;
    List<Player> players;
    int currentPlayer;

    public Vector3[] positions;

    Dictionary<int, int> joints;

    Dictionary<Player, GameObject> pieces;

    public delegate void UpdateMessage(Player player);
    public event UpdateMessage message;

    public Player currentMovingPlayer;
    
    public RollinDice rollinDice;
    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        canClick = true;
        hasGameFinished = false;
        currentPlayer = 0;
        nextTurn = true;

        SetUpPositions();
        SetUpLadders();

        myboard = new Board(joints);
        players = new List<Player>();
        pieces = new Dictionary<Player, GameObject>();

        for (int i = 0; i < 4; i++)
        {
            players.Add((Player)i);
            GameObject temp = Instantiate(gamePiece);
            pieces[(Player)i] = temp;
            temp.transform.position = startPos;
            temp.GetComponent<Piece>().SetColors((Player)i);
        }
    }
    void SetUpPositions()
    {
        positions = new Vector3[100];
        float diff = 0.51f;
        positions[0] = startPos;
        int index = 1;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                positions[index] = new Vector3(positions[index - 1].x + diff, positions[index - 1].y, positions[index - 1].z);
                index++;
            }

            positions[index] = new Vector3(positions[index - 1].x, positions[index - 1].y + diff, positions[index - 1].z);
            index++;

            for (int j = 0; j < 9; j++)
            {
                positions[index] = new Vector3(positions[index - 1].x - diff, positions[index - 1].y, positions[index - 1].z);
                index++;
            }

            if (index == 100) return;
            positions[index] = new Vector3(positions[index - 1].x, positions[index - 1].y + diff, positions[index - 1].z);
            index++;
        }
    }
    void SetUpLadders()
    {
        joints = new Dictionary<int, int> {
            { 3, 14 },
            { 39, 57 },
            { 27, 83 },
            { 49, 67 },
            { 71, 92 },
            { 86,18 },
            { 77, 35 },
            { 68, 55  },
            { 98, 28 }
            
        };
    }

    private void Update()
    {
        if(nextTurn)message(players[currentPlayer]);
        if (hasGameFinished || !canClick) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (!hit.collider) return;

            if (hit.collider.CompareTag("Die"))
            {
                //int rollResult = numberGot.RollDice();
                //MovePiece(rollResult);
                //canClick = false;

               
                
                    int roll = 1 + Random.Range(0, 6);
                    hit.collider.gameObject.GetComponent<RollinDice>().RollDice();
                    canClick = false;
               
            }
        }
    }
    public void UpdateCurrentMovingPlayer(Player player)
    {
    currentMovingPlayer = player;
    }

    public Player returncurrentplayer(){
        return currentMovingPlayer;
    }
    public void MovePiece(int rollResult)
{
    nextTurn = false;
    (Dictionary<Player,List<int>> result,List<Player> playersChanged, Dictionary<Player, bool> death) = myboard.UpdateBoard(players[currentPlayer], rollResult);
    if (result.Count == 0)
    {
        canClick = true;
        currentPlayer = (currentPlayer + 1) % players.Count;
        message(players[currentPlayer]);
        return;
    }
    pieces[players[currentPlayer]].GetComponent<Piece>().SetMovement(result[players[currentPlayer]],death[players[currentPlayer]]);
    foreach (Player otherPlayer in playersChanged)
    {
        // Process otherPlayer as needed
        pieces[otherPlayer].GetComponent<Piece>().SetMovement(result[otherPlayer],true);
    }

    canClick = true;

    if (result[players[currentPlayer]][result[players[currentPlayer]].Count - 1] == 99)
    {
        players.RemoveAt(currentPlayer);
        currentPlayer %= currentPlayer;
        if (players.Count == 1) hasGameFinished = true;
        message(players[currentPlayer]);
        return;
    }

    // Update the current moving player
    UpdateCurrentMovingPlayer(players[currentPlayer]);

    // Update currentPlayer only if the roll is not 6
    if (roll != 6)
    {
        currentPlayer = (currentPlayer + 1) % players.Count;
    }
}




}