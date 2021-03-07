using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDmgSystem : MonoBehaviour
{
    [SerializeField] Slider slider;
    int health=100;

    void Start()
    {
        slider.value = health;
    }

    // Update is called once per frame
    public void GetDamage(int dmg)
    {
        health -= dmg;
        slider.value = health;
        if (health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
