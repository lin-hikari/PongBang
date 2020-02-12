using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    TrailRenderer trail;

    Vector2 direction = Vector2.zero;

    public float speed = 7f;
    public float speedIncrease = 0.005f;

    GameMaster gm;
    ScoreManager score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();

        direction = RandomInitDirection();

        gm = GameObject.Find("Game Master").GetComponent<GameMaster>();
        score = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        rb.velocity = direction;
    }

    void FixedUpdate()
    {
        speed += speedIncrease;
        direction = direction.normalized * speed;        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            direction.x *= -1f;

            if(other.transform.position.x > 0)
            {
                sprite.color = Color.cyan;
                trail.startColor = Color.cyan;
                trail.endColor = Color.clear;
            }
            else
            {
                sprite.color = Color.magenta;
                trail.startColor = Color.magenta;
                trail.endColor = Color.clear;
            }
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

    private Vector2 RandomInitDirection()
    {
        int diagonal = (int)Random.Range(1f, 5f);

        if(diagonal == 1)
        {
            return (Vector2.right + Vector2.up).normalized * speed;
        }
        else if(diagonal == 2)
        {
            return (Vector2.right + Vector2.down).normalized * speed;
        }
        else if(diagonal == 3)
        {
            return (Vector2.left + Vector2.down).normalized * speed;
        }
        else
        {
            return (Vector2.left + Vector2.up).normalized * speed;
        }
    }
}
