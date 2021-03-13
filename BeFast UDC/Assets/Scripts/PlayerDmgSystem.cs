using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDmgSystem : MonoBehaviour
{

    [SerializeField] Slider shieldSlider;
    [SerializeField] Slider healthSlider;
    float health=60;
    float  maxHealth = 60;
    [SerializeField] float maxHealthIncreaseRate;
    [SerializeField] float maxShieldIncreaseRate;
    [SerializeField] float shieldRecoveryUpgradeRate;
    public float shieldRecoveryRate = 2;
    float shieldPoints = 100;
    float maxShieldPoints = 100;
    public bool isImmortal;
    void Awake()
    {
        health = maxHealth;
        shieldPoints = maxShieldPoints;
        healthSlider.value = health;
        shieldSlider.value = shieldPoints;
    }
    private void Start()
    {
        maxHealth += PlayerPrefs.GetInt("HealthUpgrades") * maxHealthIncreaseRate;
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
        healthSlider.value = health;
        maxShieldPoints += PlayerPrefs.GetInt("ShieldUpgrades") * maxShieldIncreaseRate;
        shieldSlider.maxValue = maxShieldPoints;
        shieldPoints = maxShieldPoints;
        shieldSlider.value = shieldPoints;
        shieldRecoveryRate+=PlayerPrefs.GetInt("ShieldUpgrades")* shieldRecoveryUpgradeRate;
    }
    private void Update()
    {
        RecoverShield();
    }
    public void GetDamage(int dmg)
    {
        if (shieldPoints >= 0&& !isImmortal)
        {
            if (shieldPoints >= dmg)
            {
                ShieldDamage(dmg);
            }
            else
            {
                float dmgDifference = dmg - shieldPoints;
                health -= dmgDifference;
                healthSlider.value=health;
                shieldPoints = 0;
                shieldSlider.value = shieldPoints;
                if (health <= 0)
                {
                    GameManager.instance.GameOver();

                }
            }
        }
    }
    public void Heal(int healAmount)
    {
       
        health +=healAmount;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        healthSlider.value = health;
    }
    public void ShieldDamage(int dmg)
    {
        if (!isImmortal&&shieldPoints>0)
        {
            shieldPoints -= dmg;
            if (shieldPoints < 0)
            {
                shieldPoints = 0;
            }
            shieldSlider.value = shieldPoints;
        }
    }
    public void ShieldRecover(int recoverAmount)
    {
        shieldPoints += recoverAmount;
        if (shieldPoints >= maxShieldPoints)
        {
            shieldPoints = maxShieldPoints;
        }
        shieldSlider.value = shieldPoints;
    }
    private void RecoverShield()
    {
        if (shieldPoints < maxShieldPoints)
        {
            shieldPoints += Time.deltaTime * shieldRecoveryRate;
            shieldSlider.value = shieldPoints;
        }
    }
}
