using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDmgSystem : MonoBehaviour
{
    [SerializeField] Slider slider;
    int health=100;
    void Start()
    {
        slider.value = health;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            GameManager.instance.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}
