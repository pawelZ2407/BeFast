using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDmgSystem : MonoBehaviour
{

    [SerializeField] Slider shieldSlider;
    [SerializeField] Slider healthSlider;
    float health=100;
    int maxHealth = 100;
    public float shieldRecoveryRate = 10;
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
        shieldSlider.value = health;
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
