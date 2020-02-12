using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;

    public float speed = 10f;

    Vector2 movement = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (transform.position.x > 0)
        {
            movement = Vector2.left * speed;
            sprite.color = Color.cyan;
        }
        else
        {
            movement = Vector2.right * speed;
            sprite.color = Color.magenta;
        }
    }

    void Update()
    {
        rb.velocity = movement;
        if((transform.position.x > 10f) || (transform.position.x < -10f))
        {
            Destroy(this.gameObject);
        }
    }
}
