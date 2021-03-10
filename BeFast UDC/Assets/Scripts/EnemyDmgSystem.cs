using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
public class EnemyDmgSystem : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject deadEffect;
    float health=100;
    void Start()
    {
        slider.value = health;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            GameManager.instance.AddScore(5);
            var deadEffectSpawn = Instantiate(deadEffect);
            deadEffectSpawn.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
