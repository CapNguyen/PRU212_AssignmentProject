using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float timeToAttack;
    private float timer;

    [SerializeField] private GameObject whipAttack;
    [SerializeField] private Vector2 whipAttackSize = new Vector2(4,2);
    [SerializeField] private int whipDamage;

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

        whipAttack.SetActive(true);
        Collider2D[] hit = Physics2D.OverlapBoxAll(whipAttack.transform.position, whipAttackSize, 0f);
        ApplyDamage(hit);
    }

    private void ApplyDamage(Collider2D[] hit)
    {
        for(int i = 0; i < hit.Length; i++)
        {
            Enemy enemy = hit[i].GetComponent<Enemy>();
            if(enemy != null) 
                enemy.TakeDamage(whipDamage);
        }
    }
}
