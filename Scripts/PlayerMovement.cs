using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    public float moveSpeed = 100;
    public float jumpSpeed = 100;
    private bool grounded;
    private bool jumpKeyPressed;
    private float horizontalMovement;
    private float verticalMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        if (jumpKeyPressed && grounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed) ;
        }

        if (horizontalMovement == 1)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if(horizontalMovement == -1)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        var horVelocityToSet = horizontalMovement * moveSpeed * Time.deltaTime;
        
        rb2d.velocity = new Vector2(horVelocityToSet, rb2d.velocity.y);

        SetAnimatorProperties();
    }

    void SetAnimatorProperties()
    {
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Horizontal Movement", Mathf.Abs(horizontalMovement));
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetBool("Invisible", true);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetBool("Invisible", false);
        }
    }

    void GetMovementInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jumpKeyPressed = Input.GetAxisRaw("Vertical") == 1 || Input.GetButtonDown("Jump");
    }
    
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    
    
}
