using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    public bool isSword;
    public bool isShield;

    public Rigidbody2D rb;

    public GameObject sword, shield;

    Vector2 movement;

    [SerializeField]
    GameManager gm;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude != 0)
            animator.SetTrigger("Run");
        else
        {
            animator.SetTrigger("Idle");
            animator.SetBool("Attack1", false);
        }

            if (movement.x > 0)
            Filp(false);
        else
            Filp(true);

    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    void Filp(bool bLeft)
    {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("potion"))
        {
            Destroy(collision.gameObject);
            gm.AddScore(50);
        }
        else if (collision.CompareTag("obstacle") &&  isSword)
        {
            animator.SetBool("Attack1", true);
            Destroy(collision.gameObject);
            gm.AddScore(50);
        }
        else if (collision.CompareTag("obstacle") && isShield)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("obstacle") && (!isShield || !isSword))
        {
            gm.GameOver();
        }
    }

    public void OnSword()
    {
        sword.SetActive(true);
        isSword = true;
    }

    public void OnShield()
    {
        shield.SetActive(true);
        isShield = true;
    }
}
