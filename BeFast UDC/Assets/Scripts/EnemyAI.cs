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
    [SerializeField] float minAngleMiss;
    [SerializeField] float maxAngleMiss;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawner;
    GameObject bullet;
    Coroutine shoot;
    bool isShooting;

    Rigidbody2D rb2d;
    AIPath path;

    List<GameObject> bulletsList = new List<GameObject>();
    List<EnemyBulletScript> bulletScript = new List<EnemyBulletScript>();
    List<CircleCollider2D> bulletsCollider = new List<CircleCollider2D>();
    void Start()
    {
        player = GameManager.instance.player;
        rb2d = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();

        for (int i = 0; i <= 5; i++)
        {
            bulletsList.Add(Instantiate(bulletPrefab, transform));
            bulletsCollider.Add(bulletsList[i].GetComponent<CircleCollider2D>());
            bulletScript.Add(bulletsList[i].GetComponent<EnemyBulletScript>());
        }
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
            int bulletIndex = 0;
            for(int i = 0; i <= bulletsList.Count - 1; i++)
            {
                if (bulletsList[i].activeSelf == false)
                {
                    bulletIndex = i;
                    bulletsList[i].SetActive(true);
                    break;
                }
            }
            bulletsList[bulletIndex].transform.parent= bulletSpawner.transform;
            bulletsList[bulletIndex].transform.position = bulletSpawner.position;
            bulletsList[bulletIndex].transform.DOScale(chargedBulletScale, bulletChargingTime);
            yield return new WaitForSeconds(bulletChargingTime);
            bulletsCollider[bulletIndex].enabled = true;
            bulletScript[bulletIndex].canShoot = true;
            bulletScript[bulletIndex].disableAfterTime = true;
            bulletsList[bulletIndex].transform.parent = null;
            bulletsList[bulletIndex].transform.rotation = enemyBody.transform.rotation;
            bulletsList[bulletIndex].transform.eulerAngles = new Vector3(0, 0, bulletsList[bulletIndex].transform.eulerAngles.z + Random.Range(minAngleMiss, maxAngleMiss));

            
        }
    }
}
