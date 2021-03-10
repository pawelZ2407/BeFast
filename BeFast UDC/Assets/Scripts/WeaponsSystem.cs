using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WeaponsSystem : MonoBehaviour
{
    LineRenderer lr;
    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform rightSpawner;
    [SerializeField] Transform middleSpawner;
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

    [SerializeField] float laserChargeTime;
    [SerializeField] float laserSpeed;
    [SerializeField] float laserDamage;
    [SerializeField] float laserDamageUpgradeRate;
    [SerializeField] GameObject laserLight;
    [SerializeField] LayerMask laserDetectionLayerMask;
    [SerializeField] Transform laserEndPoint;
    GameObject spawnedLight;

    bool isCharging;
    bool isShooting;
    public string equippedWeapon;
    Coroutine shooting;
    void Start()
    {
        equippedWeapon = "Laser";
        lr = GetComponent<LineRenderer>();
        int weaponsUpgradeLevel = PlayerPrefs.GetInt("WeaponsUpgrades");
        bulletSpeed += weaponsUpgradeLevel * bulletSpeedUpgradeRate;
        bulletChargingTime -= weaponsUpgradeLevel* chargeTimeDecreaseRate;
        secondaryModeChargingTime -= weaponsUpgradeLevel * chargeTimeDecreaseRate;
        primaryFireDamage += weaponsUpgradeLevel * damageUpgradeRateP;
        secondaryFireDamage += weaponsUpgradeLevel * damageUpgradeRateS;
        laserDamage += weaponsUpgradeLevel * laserDamageUpgradeRate;
        bulletPrefab.GetComponent<BulletScript>().damage = secondaryFireDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCharging && !isShooting)
            OnMouseButtonDown();

        if (isShooting || isCharging)
        {
            OnMouseButtonUp();
        }
        if (isShooting)
        {
            OnMouseButton();
        }

      
    }
    GameObject bullet;
    void OnMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (equippedWeapon == "Default")
            {
                shooting = StartCoroutine(StartShoot());
                isShooting = true;
            }
            if (equippedWeapon == "Laser")
            {
                shooting = StartCoroutine(LaserStart());
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            shooting = StartCoroutine(SecondaryFireMode());
        }
    }

    private void OnMouseButton()
    {
        if (Input.GetMouseButton(0))
        {
            if (equippedWeapon == "Laser")
            {
                Laser();
            }

        }
        if (Input.GetMouseButton(1))
        {

        }
    }
    void OnMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0) && isShooting)
        {
            if(bullet!=null) Destroy(bullet);
            if (spawnedLight != null) Destroy(spawnedLight);
            if (shooting != null)
            {
                StopCoroutine(shooting);
            }
            laserEndPoint.position = middleSpawner.position;
            isShooting = false;
            lr.enabled = false;
        }
        if (Input.GetMouseButtonUp(1) && isCharging)
        {
            Destroy(bullet);
            if (shooting != null)
            {
                StopCoroutine(shooting);
            }
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

            bullet.GetComponent<Rigidbody2D>().velocity=bullet.transform.up * bulletSpeed * Time.deltaTime;
        }   
    }
    IEnumerator SecondaryFireMode()
    {
        bullet = Instantiate(bulletPrefab, this.transform);
        bullet.transform.position = middleSpawner.position;
        bullet.transform.DOScale(chargedSecondaryBulletScale, secondaryModeChargingTime);
        yield return new WaitForSeconds(secondaryModeChargingTime);
        bullet.transform.parent = null;
        bullet.GetComponent<CircleCollider2D>().enabled = true;
        bullet.GetComponent<CircleCollider2D>().isTrigger = true;
        bullet.transform.rotation = this.transform.rotation;
        
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed * Time.deltaTime;
        isCharging = false;
    }
    void Laser()
    {
        lr.enabled = true;
        lr.SetPosition(0, middleSpawner.position);
        RaycastHit2D hit = Physics2D.Raycast(middleSpawner.position, transform.up, 100,laserDetectionLayerMask);


        if (Vector2.Distance(middleSpawner.position, laserEndPoint.position) < Vector2.Distance(middleSpawner.position, hit.point))
        {
            lr.SetPosition(1, laserEndPoint.position);
            laserEndPoint.transform.Translate(transform.up * laserSpeed * Time.deltaTime,Space.World);
        }
        else
        {
            lr.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyDmgSystem>().GetHeat();
            }
        }


    }
    IEnumerator LaserStart()
    {
        yield return new WaitForSeconds(laserChargeTime);
        isShooting = true;
        spawnedLight = Instantiate(laserLight, transform);
    }


}
