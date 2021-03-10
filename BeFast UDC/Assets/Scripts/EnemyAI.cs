using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;
public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public Transform enemyBody;

    public float startShootingDistance;
    public float speed;
    public float bulletChargingTime;
    public float chargedBulletScale;
    public float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawner;
    GameObject bullet;
    Coroutine shoot;
    bool isShooting;

    Rigidbody2D rb2d;
    AIPath path;

    void Start()
    {
        player = GameManager.instance.player;
        rb2d = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();
    }


    void Update()
    {
        if (Vector2.Distance(player.position,transform.position)<=startShootingDistance&&!isShooting)
        {
            shoot = StartCoroutine(Shooting());
        }
        else if(Vector2.Distance(player.position,transform.position)> startShootingDistance+1f && shoot!=null&& isShooting)
        {
            isShooting = false;
            StopCoroutine(shoot);
            Destroy(bullet);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    IEnumerator Shooting()
    {
        isShooting = true;
        while (true)
        {
            bullet = Instantiate(bulletPrefab, this.transform);
            bullet.transform.position = bulletSpawner.position;
            bullet.transform.DOScale(chargedBulletScale, bulletChargingTime);
            yield return new WaitForSeconds(bulletChargingTime);
            bullet.GetComponent<CircleCollider2D>().enabled = true;
            bullet.transform.parent = null;
            bullet.transform.rotation = enemyBody.transform.rotation;

            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed * Time.deltaTime;
        }
    }
}
