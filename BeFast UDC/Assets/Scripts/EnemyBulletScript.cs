using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    CircleCollider2D coll;
    [SerializeField] int damage;
    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"),LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"),LayerMask.NameToLayer("EnemyBullet"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerDmgSystem>().GetDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
