using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    Vector2 up = Vector2.up;
    Vector2 down = Vector2.down;

    public string upButton = "P1Up";
    public string downButton = "P1Down";
    public string shootButton = "P1Shoot";

    public float moveStrength = 500f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        up.y *= moveStrength;
        down.y *= moveStrength;
    }

    void Update()
    {
        if (Input.GetButtonDown(upButton))
        {
            rb.AddForce(up);
        }
        if (Input.GetButtonDown(downButton))
        {
            rb.AddForce(down);
        }
        if((Input.GetButtonUp(upButton))||(Input.GetButtonUp(downButton)))
        {
            rb.Sleep();
            rb.WakeUp();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            anim.SetTrigger("Hit");
        }
    }
}
