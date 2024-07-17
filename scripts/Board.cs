using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { RED, BLUE, GREEN, YELLOW }

public class Board
{
    Dictionary<Player, int> playerPos;
    int[] ladders;
    Dictionary<Player, bool> death;

    public Board(Dictionary<int, int> joints)
    {
        playerPos = new Dictionary<Player, int>();
        death = new Dictionary<Player, bool>();
        ladders = new int[100];

        for (int i = 0; i < 4; i++)
        {
            playerPos[(Player)i] = 0;
            death[(Player)i] = false;
        }

        for (int i = 0; i < 100; i++)
        {
            ladders[i] = -1;
        }

        foreach (KeyValuePair<int, int> joint in joints)
        {
            ladders[joint.Key] = joint.Value;
        }

    }

    public (Dictionary<Player,List<int>> result,List<Player> playersChanged,Dictionary<Player, bool> death) UpdateBoard(Player player, int roll)
    {
        Dictionary<Player,List<int>>  result = new Dictionary<Player,List<int>>();
        List<Player> playersChanged = new List<Player>();
        for (int i = 0; i < 4; i++)
        {
            result[(Player)i] = new List<int>();
        }
        result[player].Add(playerPos[player]);
        for (int i = 0; i < roll; i++)
        {   
            playerPos[player] += 1;
            result[player].Add(playerPos[player]);
            death[player] = false;
        }

        if (result[player][result[player].Count - 1] > 99)
        {
            playerPos[player] -= roll;
            return (new Dictionary<Player,List<int>>(),new List<Player>(),death);
        }

        //GameManager manager = GameManager.instance;

        //int currentPlayerPosition = playerPos[player];
        /*
        if (currentPlayerPosition == 3 || currentPlayerPosition == 9 || currentPlayerPosition == 13 || currentPlayerPosition == 2 || currentPlayerPosition == 5 || currentPlayerPosition == 6)
        {
            //manager.spinningWheelActive();
            death[player] = true;
        }*/

        if (ladders[result[player][result[player].Count - 1]] != -1)
        {
            playerPos[player] = ladders[result[player][result[player].Count - 1]];
            result[player].Add(playerPos[player]);

            //check if it's a snake
            if(result[player][result[player].Count - 1]<result[player][result[player].Count - 2]){
                death[player] = true;
                //Debug.Log("Snake : " + result[player][result[player].Count - 1] + " " + result[player][result[player].Count - 1]);
            } 
        }
        for (int i = 0; i < 4; i++)
        {
            if (player != (Player)i && playerPos[(Player)i] == playerPos[player])
            {
                // Add the player to the list of players to reset
                result[(Player)i].Add(playerPos[(Player)i]);
                result[(Player)i].Add(0);
                playerPos[(Player)i] = 0;
                death[(Player)i] = true;
                playersChanged.Add((Player)i);
            }
        }
        return (result, playersChanged, death);
    }
}



