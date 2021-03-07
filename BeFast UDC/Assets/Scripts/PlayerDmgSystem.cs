using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDmgSystem : MonoBehaviour
{
    [SerializeField] Slider shieldSlider;
    [SerializeField] Slider healthSlider;
    int health=100;
    public float shieldRecoveryRate = 10;
    float shieldPoints = 100;
    float maxShieldPoints = 100;
    void Start()
    {
        healthSlider.value = health;
    }

    private void Update()
    {
        RecoverShield();
    }
    public void GetDamage(int dmg)
    {
        health -= dmg;
        healthSlider.value = health;

        if (health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
    public void ShieldDamage(int dmg)
    {
        shieldPoints -= dmg;
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
