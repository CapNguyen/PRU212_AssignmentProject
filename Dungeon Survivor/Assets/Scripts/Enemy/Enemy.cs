using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int hp = 4;
    public int damage = 5;
    public int experience_reward = 400;
    public float speed;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.speed = stats.speed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform playerTransform;
    private PlayerManager playerHealth;
    private GameObject player;
    //private Animator anim;
    private Rigidbody2D rb;
    private bool isFacingRight;

    public EnemyStats stats;

    void Start()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void Update()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        if(transform.position.x < playerTransform.position.x && isFacingRight)
        {
            Flip();
        }else if(transform.position.x > playerTransform.position.x && !isFacingRight)
        {
            Flip();
        }
    }*/

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
        rb.velocity = playerDistance * stats.speed;
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
        playerHealth.TakeDamage(stats.damage);
    }

    public void TakeDamage(int dmg)
    {
        stats.hp -= dmg;

        if (stats.hp < 1)
        {
            player.GetComponent<Level>().AddExperience(stats.experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
}
