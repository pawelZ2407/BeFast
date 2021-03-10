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
    [SerializeField] float laserExplosionRange;
    [SerializeField] float explosionDamage;
    [SerializeField] float getHeatTickrate;
    [SerializeField] SpriteRenderer spriteRenderer;
    Color defaultColor;
    [SerializeField] Color tickingColor;
    float health=100;
    float maxHeatTicks=5;
    float currentHeatTicks;
    float timer;

    void Start()
    {
        currentHeatTicks = maxHeatTicks;
        slider.value = health;
        defaultColor = spriteRenderer.color;
    }
    private void Update()
    {

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
            GameManager.instance.ShakeCamera(5f,0.2f);
            Destroy(this.gameObject);
        }
    }
    public void GetHeat()
    {
        timer += Time.deltaTime;
        if (timer >= getHeatTickrate)
        {
                StartCoroutine(TickColors());
            currentHeatTicks--;
            timer = 0;
           
        }
        if (currentHeatTicks <= 0)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, laserExplosionRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    hitCollider.GetComponent<EnemyDmgSystem>().GetDamage(25);
                }
            }
            GetDamage(100);
        }
        
    }
    IEnumerator TickColors()
    {

            spriteRenderer.DOColor(tickingColor, getHeatTickrate / 2);
            yield return new WaitForSeconds(getHeatTickrate / 2);
            spriteRenderer.DOColor(defaultColor, getHeatTickrate / 2);
       
    }
}
