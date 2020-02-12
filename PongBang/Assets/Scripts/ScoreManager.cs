using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private AudioSource goal, win;

    public Text p1Score, p2Score;
    public Text p1Cheer, p2Cheer;
    private int p1, p2;

    private int winCondition;

    public GameMaster gm;

    public PlayerControl player1, player2;

    void Start()
    {
        goal = GetComponents<AudioSource>()[0];
        win = GetComponents<AudioSource>()[1];

        winCondition = gm.winCondition;
    }

    public void P1Goal()
    {
        IncreaseScore(p1Score);
        StartCoroutine(Cheer(p1Cheer));
        p1++;
        ResetPlayers();
        CheckVictory(1);
    }

    public void P2Goal()
    {
        IncreaseScore(p2Score);
        StartCoroutine(Cheer(p2Cheer));
        p2++;
        ResetPlayers();
        CheckVictory(2);
    }

    private void IncreaseScore(Text playerText)
    {
        int score = int.Parse(playerText.text);
        score++;
        playerText.text = score.ToString();
    }

    private void ResetPlayers()
    {
        player1.ResetSize();
        player2.ResetSize();
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

            win.Play();
        }
        else
        {
            goal.Play();
        }
    }

    IEnumerator Cheer(Text cheerText)
    {
        cheerText.text = "Score!";
        yield return new WaitForSeconds(2f);
        cheerText.text = "";
    }
}
