using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    CircleCollider2D coll;
    public float damage;
    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"),LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"),LayerMask.NameToLayer("Bullet"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyDmgSystem>().GetDamage(damage);
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyDmgSystem>().GetDamage(damage);
        }
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
}
