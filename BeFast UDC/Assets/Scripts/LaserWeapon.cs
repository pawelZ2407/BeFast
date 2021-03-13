using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    WeaponsSystem weaponsSystem;

    LineRenderer lr;
    [SerializeField] float laserChargeTime;
    [SerializeField] float laserSpeed;
    [SerializeField] float laserDamage;
    [SerializeField] float laserDamageUpgradeRate;
    [SerializeField] GameObject laserLight;
    [SerializeField] float lightSpawnRate;
    [SerializeField] LayerMask laserDetectionLayerMask;
    [SerializeField] Transform laserEndPoint;

    Transform middleSpawner;

    List<GameObject> lightsList = new List<GameObject>();

    void Start()
    {
        
        weaponsSystem = GetComponent<WeaponsSystem>();
        middleSpawner = weaponsSystem.middleSpawner;
        lr = GetComponent<LineRenderer>();
        int weaponsUpgradeLevel = PlayerPrefs.GetInt("WeaponsUpgrades");

        laserDamage += weaponsUpgradeLevel * laserDamageUpgradeRate;

        for (int i = 0; i <= 100; i++)
        {
            lightsList.Add(Instantiate(laserLight,middleSpawner.position,Quaternion.identity, this.transform));
        }
    }
    private void Update()
    {
        if (weaponsSystem.equippedWeapon == "Laser")
        {
            if (!weaponsSystem.isShooting && !weaponsSystem.isCharging)
            {
                OnMouseButtonDown();
            }
            if (weaponsSystem.isShooting)
            {
                OnMouseButton();
            }

            if (weaponsSystem.isShooting || weaponsSystem.isCharging)
            {
                OnMouseButtonUp();
            }
        }
     

    }
    void OnMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
                weaponsSystem.shooting = StartCoroutine(LaserStart());
            weaponsSystem.isShooting = true;
        }
    }
    void OnMouseButton()
    {
        if (Input.GetMouseButton(0))
        {
            Laser();
        }

    }
    void OnMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (weaponsSystem.shooting != null) StopCoroutine(weaponsSystem.shooting);
            weaponsSystem.isShooting = false;
            laserEndPoint.position = middleSpawner.position;
            Debug.Log("Mouse up");
            lr.enabled = false;
            CancelInvoke();
            AudioManager.instance.StopSFXSound();
            for (int i = 0; i <= lightsList.Count - 1; i++)
            {
                if (lightsList[i].activeSelf)
                {
                    lightsList[i].SetActive(false);
                }
            }
        }
       
    }
    void Laser()
    {
        lr.enabled = true;
        lr.SetPosition(0, middleSpawner.position);
        RaycastHit2D hit = Physics2D.Raycast(middleSpawner.position, transform.up, 100, laserDetectionLayerMask);


        if (Vector2.Distance(middleSpawner.position, laserEndPoint.position) < Vector2.Distance(middleSpawner.position, hit.point))
        {
            lr.SetPosition(1, laserEndPoint.position);
            laserEndPoint.transform.Translate(transform.up * laserSpeed * Time.deltaTime, Space.World);
            GameManager.instance.laserEndPosition = laserEndPoint.position;
        }
        else
        {
            lr.SetPosition(1, hit.point);
            GameManager.instance.laserEndPosition = hit.point;
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyDmgSystem>().GetHeat();
            }
        }
    }
    IEnumerator LaserStart()
    {
        //weaponsSystem.isCharging = true;
        yield return new WaitForSeconds(laserChargeTime);
        // weaponsSystem.isCharging = false;
        weaponsSystem.isShooting = true;
        AudioManager.instance.PlayConstantLaser();
        InvokeRepeating("SpawnLights", 0, lightSpawnRate);
    }
    void SpawnLights()
    {
        int lightIndex = 0;
        for(int i = 0; i <= lightsList.Count - 1; i++)
        {
            if (!lightsList[i].activeSelf)
            {
                lightIndex = i;
                break;
            }
        }
        
        lightsList[lightIndex].transform.position = middleSpawner.position;
        lightsList[lightIndex].transform.rotation = transform.rotation;
        lightsList[lightIndex].SetActive(true);
    }
    public void StopLaser()
    {

            if (weaponsSystem.shooting != null) StopCoroutine(weaponsSystem.shooting);
            weaponsSystem.isShooting = false;
            laserEndPoint.position = middleSpawner.position;
            lr.enabled = false;
            CancelInvoke();
            AudioManager.instance.StopSFXSound();
            for (int i = 0; i <= lightsList.Count - 1; i++)
            {
                if (lightsList[i].activeSelf)
                {
                    lightsList[i].SetActive(false);
                }
            }
    }
}
