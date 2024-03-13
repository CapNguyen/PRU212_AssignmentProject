using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;
    [SerializeField] private int currentHealth = 1000;
    [SerializeField] public int armor = 0;
    [SerializeField] private HealthBar playerHealth;

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;

    public float damageBonus;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead;

    [SerializeField] DataContainer dataContainer;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        ApplyPersistantUpgrades();

        playerHealth.SetStatus(currentHealth, maxHealth);
    }

    private void ApplyPersistantUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);

        maxHealth += maxHealth / 10 * hpUpgradeLevel;

        int dmgUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);

        damageBonus = 1f + 0.1f * dmgUpgradeLevel;
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if(hpRegenerationTimer > 1f)
        {
            Health(1);
            hpRegenerationTimer -= 1f;
        }
    }

    public void TakeDamage(int damage)
    {
        if(isDead) return;
        ApplyArmor(ref damage);

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Debug.Log("Chet roi");
            GetComponent<GameOver>().PlayerGameOver();
            isDead = true;
        }
        playerHealth.SetStatus(currentHealth, maxHealth);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if(damage < 0) { damage = 0; }
    }

    public void Health(int amount)
    {
        if(currentHealth <= 0)
        {
            return;
        }

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
