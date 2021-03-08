using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WeaponsSystem : MonoBehaviour
{

    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform rightSpawner;
    [SerializeField] Transform middleSpawner;
    [SerializeField] GameObject bulletPrefab;

    public float bulletSpeed;
    public float bulletChargingTime;
    public float secondaryModeChargingTime;
    public float chargedBulletScale = 0.4f;
    public float chargedSecondaryBulletScale = 1f;
    public int secondaryFireDamage;
    bool isCharging;
    bool isShooting;
    Coroutine shooting;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetMouseButtonDown(0)&&!isCharging && !isShooting )
        {
            isShooting = true;
            shooting = StartCoroutine(StartShoot());
        }
        if (Input.GetMouseButtonDown(1) &&!isCharging&& !isShooting)
        {
            isCharging = true;
            shooting = StartCoroutine(SecondaryFireMode());
        }
        if (Input.GetMouseButtonUp(0)&& isShooting)
        {
            Destroy(bullet);
            StopCoroutine(shooting);
            isShooting = false;
        }
        if (Input.GetMouseButtonUp(1)&&isCharging)
        {
            Destroy(bullet);
            StopCoroutine(shooting);
            isCharging = false;
        }
    }
    GameObject bullet;

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
        bullet.GetComponent<BulletScript>().damage = secondaryFireDamage;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed * Time.deltaTime;
        isCharging = false;
    }
}
