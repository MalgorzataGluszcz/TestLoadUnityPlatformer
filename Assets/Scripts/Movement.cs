using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float jumpForce = 0.0f;

    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private LayerMask groundLayer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        
        if (moveX > 0.01f)
            transform.localScale = Vector3.one;
        else if (moveX < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        animator.SetBool("walk", moveX != 0);

        rb2D.velocity = new Vector2(moveX * speed, rb2D.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            animator.SetTrigger("jump");
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D rayCast2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0.0f, Vector2.down, 0.1f, groundLayer);
        return rayCast2D.collider != null;
    }
}
