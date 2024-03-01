using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;
    [SerializeField] private int currentHealth = 1000;
    [SerializeField] private HealthBar playerHealth;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        playerHealth.SetStatus(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Debug.Log("Player is dead");
        }
        playerHealth.SetStatus(currentHealth, maxHealth);
    }

    public void Health(int amount)
    {
        if(currentHealth <= 0)
        {
            return;
        }

        currentHealth += amount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
