using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text p1Score, p2Score;
    private int p1, p2;

    private int winCondition;

    public GameMaster gm;

    void Start()
    {
        winCondition = gm.winCondition;
    }

    public void P1Goal()
    {
        IncreaseScore(p1Score);
        p1++;
        CheckVictory(1);
    }

    public void P2Goal()
    {
        IncreaseScore(p2Score);
        p2++;
        CheckVictory(2);
    }

    private void IncreaseScore(Text playerText)
    {
        int score = int.Parse(playerText.text);
        score++;
        playerText.text = score.ToString();
    }

    private void CheckVictory(int playerNumber)
    {
        int score = 0;

        if (playerNumber == 1) score = p1;
        else if (playerNumber == 2) score = p2;
        else Debug.Log("Invalid input on method!");

        if(score >= winCondition)
        {
            gm.EndGame(playerNumber);
        }
    }
}
