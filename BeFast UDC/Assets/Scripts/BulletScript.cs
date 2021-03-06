using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    CircleCollider2D coll;
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"),LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"),LayerMask.NameToLayer("Bullet"));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(this.gameObject);
    }
}
