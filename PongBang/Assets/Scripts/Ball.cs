using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 direction = Vector2.one;

    public float speed = 5f;

    GameMaster gm;
    ScoreManager score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        direction *= speed;

        gm = GameObject.Find("Game Master").GetComponent<GameMaster>();
        score = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        rb.velocity = direction;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            direction.x *= -1f;
        }
        else if (other.gameObject.tag == "Wall")
        {
            direction.y *= -1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Goal")
        {
            if (other.gameObject.transform.position.x > 0f)
            {
                score.P1Goal();
            }
            else
            {
                score.P2Goal();
            }

            gm.WaitAndSpawnBall();

            Destroy(this.gameObject);
        }
    }
}
