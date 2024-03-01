using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int hp = 4;
    [SerializeField] private int damage = 5;
    [SerializeField] private int experience_reward = 400;

    private PlayerManager playerHealth;
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isFacingRight;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        if(transform.position.x < playerTransform.position.x && isFacingRight)
        {
            Flip();
        }else if(transform.position.x > playerTransform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    public void SetTarget(GameObject _player)
    {
        this.player = _player;
        this.playerTransform = _player.transform;
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void FixedUpdate()
    {
        Vector3 playerDistance = (playerTransform.position - transform.position).normalized;
        rb.velocity = playerDistance * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(playerHealth == null)
        {
            playerHealth = player.GetComponent<PlayerManager>(); 
        }
        playerHealth.TakeDamage(damage);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp < 1)
        {
            player.GetComponent<Level>().AddExperience(experience_reward);
            Destroy(gameObject);
        }
    }
}
