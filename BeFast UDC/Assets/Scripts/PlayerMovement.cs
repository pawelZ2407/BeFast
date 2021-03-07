using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] Animator fireAnimator;
    Camera cam;
    Vector2 move;
    public float speed = 5;
    public float fastSpeedMultiplier;
    public float rotationSpeed = 10;

    public int scoreForCoin;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float yMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        move = new Vector2(xMove, yMove);
        Vector2.ClampMagnitude(move, speed);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move*= fastSpeedMultiplier;
            fireAnimator.speed = 1.5f;
            Vector2.ClampMagnitude(move, speed * fastSpeedMultiplier);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            fireAnimator.speed = 1;
        }

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {

        rb2d.velocity = move;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(scoreForCoin);
            Destroy(collision.gameObject);
            GameManager.instance.SpawnCoin();
        }
    }
}
