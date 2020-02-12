using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public Text winText;

    public Text p1Help, p2Help;

    public Text restartText;

    public int winCondition = 5;

    public Object ball;

    float initBallSpawn = 5f;
    float ballRespawn = 2f;

    bool gameOver = false;

    void Start()
    {
        StartCoroutine(TellWinCondition());

        WaitAndSpawnBall(initBallSpawn);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if(gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator TellWinCondition()
    {
        for(int i = 1; i <= 4; i++)
        {
            yield return new WaitForSeconds(0.5f);
            winText.text = "First to " + winCondition.ToString() + " wins!";
            yield return new WaitForSeconds(0.5f);
            winText.text = "";                        
        }

        p1Help.text = "";
        p2Help.text = "";
    }

    public void WaitAndSpawnBall()
    {
        StartCoroutine(SpawnBallDelay(ballRespawn));
    }
    public void WaitAndSpawnBall(float delay)
    {
        StartCoroutine(SpawnBallDelay(delay));
    }

    IEnumerator SpawnBallDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if(!gameOver)
        {
            Instantiate(ball, Vector2.zero, Quaternion.identity);
        }        
    }

    public void EndGame(int winningPlayer)
    {
        gameOver = true;

        if (winningPlayer == 1) winText.color = Color.magenta;
        else if (winningPlayer == 2) winText.color = Color.cyan;
        else Debug.Log("Invalid input on method!");

        winText.text = "Player " + winningPlayer.ToString() + " wins!";
        restartText.text = "Press Enter to play again!";
    }
}
