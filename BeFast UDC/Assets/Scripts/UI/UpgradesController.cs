using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradesController : MonoBehaviour
{
    [SerializeField] Slider speedSlider;
    [SerializeField] TMP_Text speedPriceText;
    int speedUpgradesAmount=1;
    int speedUpgradePrice=500;
    int speedUpgradeDefaultPrice;


     Slider weaponsSlider;
     int weaponsUpgradesAmount;
     int weaponsUpgradePrice;

     Slider shieldSlider;
     int shieldUpgradesAmount;
     int shieldUpgradePrice;
    
     Slider healthSlider;
     int healthUpgradesAmount;
     int healthUpgradePrice;

     Slider powerUpsSlider;
     int powerUpsUpgradesAmount;
     int powerUpsUpgradePrice;
    
     Slider inventorySizeSlider;
     int inventorySizeUpgradesAmount;
     int inventorySizeUpgradePrice;

    private void Start()
    {
        speedUpgradeDefaultPrice = speedUpgradePrice;
        speedPriceText.text = speedUpgradePrice.ToString();
        PlayerPrefs.SetInt("Money", 100000);
        Debug.Log("Money: " + PlayerPrefs.GetInt("Money"));
    }
    public void UpgradeSpeed()
    {
        if (PlayerPrefs.GetInt("Money") >= speedUpgradePrice&& speedUpgradesAmount<speedSlider.maxValue)
        {
            speedUpgradesAmount++;
            PlayerPrefs.SetFloat("Speed", GameManager.instance.playerSpeed + speedUpgradesAmount*10);
            speedSlider.value = speedUpgradesAmount;
            int currentMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetInt("Money",currentMoney-speedUpgradePrice );
            speedUpgradePrice = speedUpgradeDefaultPrice+ speedUpgradeDefaultPrice*speedUpgradesAmount;
            speedPriceText.text = speedUpgradePrice.ToString();

            Debug.Log("Money: " + PlayerPrefs.GetInt("Money"));
        }
        else
        {
            if(PlayerPrefs.GetInt("Money") <= speedUpgradePrice)
            {
                Debug.Log("You need more money");
            }
            else if(speedUpgradesAmount < speedSlider.maxValue)
            {
                Debug.Log("You reached maximumspeed level!");
            }

        }
    }
    public void UpgradeWeapons()
    {

    }
    public void UpgradeShields()
    {

    }
    public void UpgradeHealth()
    {

    }
    public void UpgradePowerUps()
    {

    }
    public void UpgradeInventorySize()
    {

    }
}
