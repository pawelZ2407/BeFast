using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerDmgSystem playerDmgSys;
    WeaponsSystem weaponsSystem;
    [SerializeField] int scoreForCoin;
    [SerializeField] int healthPackHeal;
    [SerializeField] int shieldPackPoints;
    [SerializeField] float immortalDuration;
    [SerializeField] float immortalDurationIncreaseRate;
    [SerializeField] GameObject forceField;
    void Start()
    {
        playerDmgSys = GetComponent<PlayerDmgSystem>();
        weaponsSystem = GetComponent<WeaponsSystem>();
        immortalDuration+= PlayerPrefs.GetInt("PowerUpsUpgrades")* immortalDurationIncreaseRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
        if (collision.CompareTag("LaserWeapon"))
        {
            weaponsSystem.equippedWeapon = "Laser";
            Destroy(collision.gameObject);
            GameManager.instance.isPowerUpSpawned = false;
        }
        if(collision.CompareTag("DefaultWeapon")){
            GetComponent<LaserWeapon>().StopLaser();
            weaponsSystem.equippedWeapon = "Default";
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
