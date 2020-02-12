using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    TrailRenderer trail;
    AudioSource sound;

    Vector2 movement = Vector2.zero;

    public float speed = 7f;
    public float speedIncrease = 0.005f;

    GameMaster gm;
    ScoreManager score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        sound = GetComponent<AudioSource>();

        movement = RandomInitDirection();

        gm = GameObject.Find("Game Master").GetComponent<GameMaster>();
        score = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        rb.velocity = movement;
    }

    void FixedUpdate()
    {
        speed += speedIncrease;
        movement = movement.normalized * speed;        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            movement.x *= -1f;

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

            sound.Play();
        }
        else if (other.gameObject.tag == "Wall")
        {
            movement.y *= -1f;

            sound.Play();
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
