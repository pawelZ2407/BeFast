using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Transform parent;
    private Vector2 defaultScale;
    CircleCollider2D coll;
    public bool canShoot;
    public bool disableAfterTime = false;

    private void Start()
    {
        parent = this.transform.parent;
        coll = GetComponent<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"));
        defaultScale = transform.lossyScale;
    }
    private void Update()
    {
        if (canShoot)
        {

            transform.position += transform.up * bulletSpeed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canShoot = false;
            collision.collider.GetComponent<PlayerDmgSystem>().GetDamage(10);
            this.gameObject.SetActive(false);
        } 
        if (collision.collider.CompareTag("Obstacle"))
        {
            canShoot = false;
            this.gameObject.SetActive(false);
        }
    }
}
