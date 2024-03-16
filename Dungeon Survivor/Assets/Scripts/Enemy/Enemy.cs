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
    [SerializeField] EnemyData enemyData;

    float stunned;
    Vector3 knockbackVector;
    float knockbackForce;
    float knockbackTimeWeight;
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        if (enemyData != null)
        {
            InitSprite(enemyData.animatedPrefab);
            SetStats(enemyData.stats);
            SetTarget(GameManager.instance.playerTranform.gameObject);
        }
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
        ProcessStun();
        Move();
    }

    private void ProcessStun()
    {
        if (stunned > 0f)
        {
            stunned -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (stunned > 0f)
        {
            return;
        }
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = CalculateMovementVelocity(direction) + CalculateKnockback();
    }

    private Vector3 CalculateMovementVelocity(Vector3 playerDistance)
    {
        return playerDistance * stats.speed * (stunned > 0f ? 0f : 1f);
    }

    private Vector3 CalculateKnockback()
    {
        if (knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        }
        return knockbackVector * knockbackTimeWeight * (knockbackTimeWeight > 0f ? 1f : 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (playerHealth == null)
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
    internal void InitSprite(GameObject animatedPrefabs)
    {
        GameObject spriteObject = Instantiate(animatedPrefabs);
        spriteObject.transform.parent = transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }

    public void Stun(float stun)
    {
        stunned = stun;
    }

    public void Knockback(Vector3 vector, float force, float timeWeight)
    {
        knockbackVector = vector;
        knockbackForce = force;
        knockbackTimeWeight = timeWeight;
    }
}
