using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;

    public static PlayerStats instance { get { return _instance; } }

    public float speed;
    public float weaponsUpgrades;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        if (PlayerPrefs.HasKey("Speed"))
        {
            speed = PlayerPrefs.GetFloat("Speed");
        }
        else
        {
            speed = 200;
            PlayerPrefs.SetFloat("Speed", speed);
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
