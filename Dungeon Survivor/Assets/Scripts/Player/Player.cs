using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] private float speed;
    [HideInInspector]
    public Vector2 movement;
    private bool facingRight = true;
    private float xInput;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
    }

    void Update()
    {
        AnimatorController();
        MovementController();
        FlipController();
    }

    private void AnimatorController()
    {
        anim.SetFloat("xVelocity", xInput);
    }

    private void FlipController()
    {
        if(facingRight && rb.velocity.x < 0)
        {
            Flip();
        }else if(!facingRight && rb.velocity.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0,180,0);
        facingRight = !facingRight;
    }

    private void MovementController()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        movement.x = xInput;
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = movement * speed;
    }
}
