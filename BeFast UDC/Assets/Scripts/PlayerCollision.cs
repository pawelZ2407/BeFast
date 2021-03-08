using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerDmgSystem playerDmgSys;

    public int scoreForCoin;
    public int healthPackHeal;
    public int shieldPackPoints;
    public int immortalDuration;

    [SerializeField] GameObject forceField;
    void Start()
    {
        playerDmgSys = GetComponent<PlayerDmgSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(scoreForCoin);
            Destroy(collision.gameObject);
            GameManager.instance.SpawnCoin();
        }
        if (collision.CompareTag("HealthPack"))
        {
            playerDmgSys.Heal(healthPackHeal);
            Destroy(collision.gameObject);
            GameManager.instance.isPowerUpSpawned = false;
        }
        if (collision.CompareTag("Shield"))
        {
            playerDmgSys.ShieldRecover(shieldPackPoints);
            Destroy(collision.gameObject);
            GameManager.instance.isPowerUpSpawned = false;
        }
        if (collision.CompareTag("ForceField"))
        {
            StartCoroutine(ShieldImmortality());
            Destroy(collision.gameObject);
            GameManager.instance.isPowerUpSpawned = false;
        }
    }
    IEnumerator ShieldImmortality()
    {

        playerDmgSys.isImmortal = true;
        forceField.SetActive(true);
        yield return new WaitForSeconds(immortalDuration);
        playerDmgSys.isImmortal = false;
        forceField.SetActive(false);
    }
}
