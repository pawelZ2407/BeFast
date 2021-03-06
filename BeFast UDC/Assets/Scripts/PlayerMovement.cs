using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    PlayerDmgSystem playerDmgSys;

    Rigidbody2D rb2d;
    [SerializeField] Animator fireAnimator;
    [SerializeField] Slider fuelSlider;
    [SerializeField] Slider shieldSlider;
    Camera cam;
    Vector2 move;
    public float speed = 5;
    public float speedAddedPerUpgrade = 10;
    public float fastSpeedMultiplier;
    public float rotationSpeed = 10;
    public float fuel = 100;
    public float maxFuel;
    public float fuelDecreaseRate;
    public float fuelRecoverRate;
    public int boostHitDamage;
    private bool isBoosting;

    [SerializeField] float fuelDecreaseUpgradeRate;
    [SerializeField] float fuelRecoverUpgradeRate;

    [SerializeField] GameObject forceField;
    private void Awake()
    {
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        playerDmgSys = GetComponent<PlayerDmgSystem>();
        speed +=PlayerPrefs.GetInt("SpeedUpgrades")*speedAddedPerUpgrade;
        int boostUpgrades = PlayerPrefs.GetInt("SpeedBoosterUpgrades");
        fuelDecreaseRate -= boostUpgrades * fuelDecreaseUpgradeRate;
        fuelRecoverRate += boostUpgrades * fuelRecoverUpgradeRate;
        fuel = maxFuel;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal") * speed;
        float yMove = Input.GetAxisRaw("Vertical") * speed;
        move = new Vector2(xMove, yMove);
        Vector2.ClampMagnitude(move, speed);
        if (Input.GetKey(KeyCode.LeftShift)&&fuel>1)
        {
            isBoosting = true;
            move*= fastSpeedMultiplier;
            fireAnimator.speed = 1.5f;
            Vector2.ClampMagnitude(move, speed * fastSpeedMultiplier);
            fuel -= Time.deltaTime * fuelDecreaseRate;
            fuelSlider.value = fuel;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || fuel<=0)
        {
            fireAnimator.speed = 1;
            isBoosting = false;
        }
        if (!isBoosting && fuel<=maxFuel)
        {
            fuel += Time.deltaTime * fuelRecoverRate;
            fuelSlider.value = fuel;
        }
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {

        rb2d.velocity = move*Time.deltaTime;
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && isBoosting&&!forceField.activeSelf)
        {
            collision.collider.GetComponent<EnemyDmgSystem>().GetDamage(boostHitDamage);
            if (shieldSlider.value > 0)
            {
                playerDmgSys.ShieldDamage(boostHitDamage / 4);
            }
            else
            {
                playerDmgSys.GetDamage(boostHitDamage / 4);
            }


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")&& isBoosting && forceField.activeSelf)
        {
            collision.GetComponent<EnemyDmgSystem>().GetDamage(boostHitDamage);
        }
    }


}
