using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    AudioSource shoot, hit;

    Vector2 up = Vector2.up;
    Vector2 down = Vector2.down;

    public string upButton = "P1Up";
    public string downButton = "P1Down";
    public string shootButton = "P1Shoot";

    public float moveStrength = 500f;

    public Object bullet;

    public float bulletSpawnDistance = 0.5f;

    public float shootCooldown = 0.5f;

    private bool canShoot = true;

    private float size;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shoot = GetComponents<AudioSource>()[0];
        hit = GetComponents<AudioSource>()[1];

        up.y *= moveStrength;
        down.y *= moveStrength;

        size = transform.localScale.y;
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

        if(Input.GetButtonDown(shootButton))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(canShoot)
        {
            StartCoroutine(ShootAndCooldown());
        }
    }

    IEnumerator ShootAndCooldown()
    {
        canShoot = false;

        //do the shot
        Vector2 playerPos = this.gameObject.transform.position;
        float spawnX;
        if(playerPos.x > 0)
        {
            spawnX = playerPos.x - bulletSpawnDistance;
        }
        else
        {
            spawnX = playerPos.x + bulletSpawnDistance;
        }
        Vector2 spawnPos = new Vector2(spawnX, playerPos.y);
        Instantiate(bullet, spawnPos, Quaternion.identity);

        shoot.Play();

        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }

    public void ResetSize()
    {
        size = 2f;
        transform.localScale = new Vector2(transform.localScale.x, size);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            anim.SetTrigger("Hit");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            size -= 0.2f;
            transform.localScale = new Vector2(transform.localScale.x, size);
            Destroy(other.gameObject);

            hit.Play();
        }
    }
}
