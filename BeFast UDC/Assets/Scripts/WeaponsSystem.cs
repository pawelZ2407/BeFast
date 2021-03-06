﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WeaponsSystem : MonoBehaviour
{
    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform rightSpawner;
    [SerializeField] GameObject bulletPrefab;

    public float bulletSpeed;
    public float bulletChargingTime;
    public float chargedBulletScale = 0.4f;
    Coroutine shoot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot = StartCoroutine(StartShoot());
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(bullet);
            StopCoroutine(shoot);
        }
    }
    GameObject bullet;
    IEnumerator StartShoot()
    {
        int turretNumber = 0;
        while (true)
        {
            bullet = Instantiate(bulletPrefab);
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
            bullet.transform.rotation = this.transform.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse); 
        }   
    }
}