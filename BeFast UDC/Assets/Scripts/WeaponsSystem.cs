using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WeaponsSystem : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform rightSpawner;
    public Transform middleSpawner;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float bulletSpeedUpgradeRate;
    [SerializeField] float chargeTimeDecreaseRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletChargingTime;
    [SerializeField] float secondaryModeChargingTime;
    [SerializeField] float chargedBulletScale = 0.4f;
    [SerializeField] float chargedSecondaryBulletScale = 1f;
    [SerializeField] float primaryFireDamage;
    [SerializeField] float damageUpgradeRateP;
    [SerializeField] float damageUpgradeRateS;
    [SerializeField] float secondaryFireDamage;


    public bool isCharging;
    public bool isShooting;
    public string equippedWeapon;
    public Coroutine shooting;
    void Start()
    {
        equippedWeapon = "Laser";
        int weaponsUpgradeLevel = PlayerPrefs.GetInt("WeaponsUpgrades");
        bulletSpeed += weaponsUpgradeLevel * bulletSpeedUpgradeRate;
        bulletChargingTime -= weaponsUpgradeLevel* chargeTimeDecreaseRate;
        secondaryModeChargingTime -= weaponsUpgradeLevel * chargeTimeDecreaseRate;
        primaryFireDamage += weaponsUpgradeLevel * damageUpgradeRateP;
        secondaryFireDamage += weaponsUpgradeLevel * damageUpgradeRateS;
      
        bulletPrefab.GetComponent<BulletScript>().damage = secondaryFireDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (equippedWeapon == "Default")
        {
            if (!isCharging && !isShooting)
            {
                OnMouseButtonDown();
            }
            if (isShooting || isCharging)
            {
                OnMouseButtonUp();
            }
        }
    }
    GameObject bullet;
    void OnMouseButtonDown()
    {

            if (Input.GetMouseButtonDown(0))
            {
                shooting = StartCoroutine(StartShoot());
                isShooting = true;
            }
   
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            shooting = StartCoroutine(SecondaryFireMode());
        }
    }

    void OnMouseButtonUp()
    {
            if (Input.GetMouseButtonUp(0) && isShooting)
            {
                if (bullet != null) Destroy(bullet);

                if (shooting != null)
                {
                    StopCoroutine(shooting);
                }

                isShooting = false;

            }

      
        if (Input.GetMouseButtonUp(1) && isCharging)
        {
            Destroy(bullet);
            if (shooting != null)
            {
                StopCoroutine(shooting);
            }
            AudioManager.instance.StopSFXSound();
            isCharging = false;
        }
    }
    IEnumerator StartShoot()
    {
        int turretNumber = 0;
        while (true)
        {
            bullet = Instantiate(bulletPrefab,this.transform);
            
            if (turretNumber == 0)
            {
                bullet.transform.position = leftSpawner.position;
                turretNumber = 1;
            }
            else if (turretNumber == 1)
            {
                bullet.transform.position = rightSpawner.position;
                turretNumber = 0;
            }
            bullet.transform.DOScale(chargedBulletScale, bulletChargingTime);
            yield return new WaitForSeconds(bulletChargingTime);
            bullet.transform.parent = null;
            bullet.GetComponent<CircleCollider2D>().enabled = true;
            bullet.transform.rotation = this.transform.rotation;
            AudioManager.instance.PlaySmallDefault();
            bullet.GetComponent<Rigidbody2D>().velocity=bullet.transform.up * bulletSpeed * Time.deltaTime;
        }   
    }
    IEnumerator SecondaryFireMode()
    {
        AudioManager.instance.PlayBigCharge();
        bullet = Instantiate(bulletPrefab, this.transform);
        bullet.transform.position = middleSpawner.position;
        bullet.transform.DOScale(chargedSecondaryBulletScale, secondaryModeChargingTime);
        yield return new WaitForSeconds(secondaryModeChargingTime);
        GameManager.instance.ShakeCamera(10, 0.2f);
        AudioManager.instance.StopSFXSound();
        AudioManager.instance.PlayBigDefault();
        bullet.transform.parent = null;
        bullet.GetComponent<CircleCollider2D>().enabled = true;
        bullet.GetComponent<CircleCollider2D>().isTrigger = true;
        bullet.transform.rotation = this.transform.rotation;
        
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed * Time.deltaTime;

        isCharging = false;
    }
  
  


}
