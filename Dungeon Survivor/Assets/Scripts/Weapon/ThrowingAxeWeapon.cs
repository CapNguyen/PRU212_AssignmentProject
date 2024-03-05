using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack;
    float timer;
    [SerializeField] GameObject axePrefab;
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        SpawnAxe();
    }

    private void SpawnAxe()
    {
        GameObject throwingAxe = Instantiate(axePrefab);
        throwingAxe.transform.position = transform.position;
        throwingAxe.GetComponent<ThrowingAxeProjectile>().setDirection(player.lastHorizontalMove, 0f);
    }
}
