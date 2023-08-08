using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;

    public Rigidbody2D rb;

    public Vector2 moveDirection;

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
        float _moveHor = Input.GetAxisRaw("Horizontal");
        float _moveVer = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(_moveHor, _moveVer).normalized;
        //Debug.Log(moveDirection.magnitude);
        if (moveDirection.magnitude != 0)
            animator.SetTrigger("Run");
        else
            animator.SetTrigger("Idle");

        Debug.Log(moveDirection.x);
        if (moveDirection.x > 0)
            Filp(false);
        else
            Filp(true);

    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void Filp(bool bLeft)
    {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);

    }
}
