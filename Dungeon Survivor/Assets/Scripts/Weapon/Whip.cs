using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    [SerializeField] private float timeToAttack;
    private float timer;

    private Player player;
    [SerializeField] private GameObject whipAttackLeft;
    [SerializeField] private GameObject whipAttackRight;
    [SerializeField] private Vector2 whipAttackSize = new Vector2(2,2);
    [SerializeField] private int whipDamage;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = timeToAttack;

        if(player.lastHorizontalMove < 0)
        {
            whipAttackLeft.SetActive(true);
            Collider2D[] hit = Physics2D.OverlapBoxAll(whipAttackLeft.transform.position, whipAttackSize, 0f);
            ApplyDamage(hit);
        }
        else
        {
            whipAttackRight.SetActive(true);
            Collider2D[] hit = Physics2D.OverlapBoxAll(whipAttackRight.transform.position, whipAttackSize, 0f);
            ApplyDamage(hit);
        }       
       
    }

    private void ApplyDamage(Collider2D[] hit)
    {
        for(int i = 0; i < hit.Length; i++)
        {
            IDamageable enemy = hit[i].GetComponent<IDamageable>();
            if(enemy != null)
            {
                enemy.TakeDamage(whipDamage);
            }
        }
    }
}
