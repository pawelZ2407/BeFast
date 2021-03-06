using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradesController : MonoBehaviour
{
    [SerializeField] GameObject moneyText;

    [SerializeField] GameObject UpgradesScreen;
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] Slider speedSlider;
    [SerializeField] TMP_Text speedPriceText;
    int speedUpgradesAmount = 1;
    int[] speedPrices = new int[15] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000 };

    [SerializeField] Slider weaponsSlider;
    [SerializeField] TMP_Text weaponsPriceText;
    int weaponsUpgradesAmount = 1;
    int[] weaponsPrices = new int[10] { 1000,2000,3000,4000,5000,6000,7000,8000,9000,10000 };
    
    [SerializeField] Slider shieldSlider;
    [SerializeField] TMP_Text shieldPriceText;
    int shieldUpgradesAmount = 1;
    int[] shieldPrices = new int[10] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000};

    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthPriceText;
    int healthUpgradesAmount = 1;
    int[] healthPrices = new int[10] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000};

    [SerializeField] Slider powerUpsSlider;
    [SerializeField] TMP_Text powerUpsPriceText;
    int powerUpsUpgradesAmount = 1;
    int[] powerUpsPrices = new int[5] { 1000, 2000, 3000, 4000, 5000 };
    
    [SerializeField] Slider speedBoosterSlider;
    [SerializeField] TMP_Text speedBoosterPriceText;
    int speedBoosterUpgradesAmount = 1;
    int[] speedBoosterPrices = new int[10] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };


    // To implement later
    Slider inventorySizeSlider;
    int inventorySizeUpgradesAmount;
    int inventorySizeUpgradePrice;

    string maximumLevelText = "MAXIMUM LEVEL";
    private void Awake()
    {
        Initializer();
    }

    private void Start()
    {
        moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();

        speedSlider.value = speedUpgradesAmount;
        
        if(speedUpgradesAmount < speedPrices.Length)
        {
            speedPriceText.text = speedPrices[speedUpgradesAmount - 1].ToString();
        }
        else
        {
            speedPriceText.text = maximumLevelText;
        }

        weaponsSlider.value = weaponsUpgradesAmount;
        if (weaponsUpgradesAmount < weaponsPrices.Length)
        {
            weaponsPriceText.text = weaponsPrices[weaponsUpgradesAmount - 1].ToString();
        }
        else
        {
            weaponsPriceText.text = maximumLevelText;
        }


        shieldSlider.value = shieldUpgradesAmount;
        if (shieldUpgradesAmount < shieldPrices.Length)
        {
            shieldPriceText.text = shieldPrices[shieldUpgradesAmount - 1].ToString();
        }
        else
        {
            shieldPriceText.text = maximumLevelText;
        }

        healthSlider.value = healthUpgradesAmount;
        if (healthUpgradesAmount < healthPrices.Length)
        {
            healthPriceText.text = healthPrices[healthUpgradesAmount - 1].ToString();
        }
        else
        {
            healthPriceText.text = maximumLevelText;
        }

        powerUpsSlider.value = powerUpsUpgradesAmount;
        if (powerUpsUpgradesAmount < powerUpsPrices.Length)
        {
            powerUpsPriceText.text = powerUpsPrices[powerUpsUpgradesAmount - 1].ToString();
        }
        else
        {
            powerUpsPriceText.text = maximumLevelText;
        }

        speedBoosterSlider.value = speedBoosterUpgradesAmount;
        if (speedBoosterUpgradesAmount < speedBoosterPrices.Length)
        {
            speedBoosterPriceText.text = speedBoosterPrices[speedBoosterUpgradesAmount - 1].ToString();
        }
        else
        {
            speedBoosterPriceText.text = maximumLevelText;
        }
        //PlayerPrefs.SetInt("Money", 100000);

        Debug.Log("Money: " + PlayerPrefs.GetInt("Money"));
    }
    void Initializer()
    {
        // Speed
        if (PlayerPrefs.HasKey("SpeedUpgrades"))
        {
            speedUpgradesAmount = PlayerPrefs.GetInt("SpeedUpgrades");
        }
        else
        {
            speedUpgradesAmount = 1;
        }
        // Weapons
        if (PlayerPrefs.HasKey("WeaponsUpgrades"))
        {
            weaponsUpgradesAmount = PlayerPrefs.GetInt("WeaponsUpgrades");
        }
        else
        {
            weaponsUpgradesAmount = 1;
        }
        if (PlayerPrefs.HasKey("ShieldUpgrades"))
        {
            shieldUpgradesAmount = PlayerPrefs.GetInt("ShieldUpgrades");
        }
        else
        {
            shieldUpgradesAmount = 1;
        }
        if (PlayerPrefs.HasKey("HealthUpgrades"))
        {
            healthUpgradesAmount = PlayerPrefs.GetInt("HealthUpgrades");
        }
        else
        {
            healthUpgradesAmount = 1;
        }
        if (PlayerPrefs.HasKey("PowerUpsUpgrades"))
        {
            powerUpsUpgradesAmount = PlayerPrefs.GetInt("PowerUpsUpgrades");
        }
        else
        {
            powerUpsUpgradesAmount = 1;
        }
        if (PlayerPrefs.HasKey("SpeedBoosterUpgrades"))
        {
            speedBoosterUpgradesAmount = PlayerPrefs.GetInt("SpeedBoosterUpgrades");
        }
        else
        {
            speedBoosterUpgradesAmount = 1;
        }
    }
   
    public void UpgradeSpeed()
    {
        if (speedUpgradesAmount < speedSlider.maxValue)
        {
            if (PlayerPrefs.GetInt("Money") >= speedPrices[speedUpgradesAmount-1])
            {
                RemoveMoney(speedPrices[speedUpgradesAmount-1]);
                moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
                speedUpgradesAmount++;
                speedSlider.value = speedUpgradesAmount;
                speedPriceText.text = speedPrices[speedUpgradesAmount-1].ToString();
                if (speedUpgradesAmount == speedSlider.maxValue)
                {
                    speedPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("SpeedUpgrades", speedUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") < speedPrices[speedUpgradesAmount-1])
                {
                    Debug.Log("You need more money");
                }
            }
        }
        else
        {
            speedPriceText.text = "MAXIMUM LEVEL";
        }
    }
    public void UpgradeWeapons()
    {
        if (weaponsUpgradesAmount < weaponsSlider.maxValue)
        {
            if (PlayerPrefs.GetInt("Money") >= weaponsPrices[weaponsUpgradesAmount - 1])
            {
                RemoveMoney(weaponsPrices[weaponsUpgradesAmount - 1]);
                moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
                weaponsUpgradesAmount++;
                weaponsSlider.value = weaponsUpgradesAmount;
                weaponsPriceText.text = weaponsPrices[weaponsUpgradesAmount - 1].ToString();
                if (weaponsUpgradesAmount == weaponsSlider.maxValue)
                {
                    weaponsPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("WeaponsUpgrades", weaponsUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") < weaponsPrices[weaponsUpgradesAmount - 1])
                {
                    Debug.Log("You need more money");
                }
            }
        }
        else
        {
            weaponsPriceText.text = "MAXIMUM LEVEL";
        }
    }
    public void UpgradeShields()
    {
        if (shieldUpgradesAmount < shieldSlider.maxValue)
        {
            if (PlayerPrefs.GetInt("Money") >= shieldPrices[shieldUpgradesAmount - 1])
            {
                RemoveMoney(shieldPrices[shieldUpgradesAmount - 1]);
                moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
                shieldUpgradesAmount++;
                shieldSlider.value = shieldUpgradesAmount;
                shieldPriceText.text = shieldPrices[shieldUpgradesAmount - 1].ToString();
                if (shieldUpgradesAmount == shieldSlider.maxValue)
                {
                    shieldPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("ShieldUpgrades", shieldUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") < shieldPrices[shieldUpgradesAmount - 1])
                {
                    Debug.Log("You need more money");
                }
            }
        }
        else
        {
            shieldPriceText.text = "MAXIMUM LEVEL";
        }
    }
    public void UpgradeHealth()
    {
        if (healthUpgradesAmount < healthSlider.maxValue)
        {
            if (PlayerPrefs.GetInt("Money") >= healthPrices[healthUpgradesAmount - 1])
            {
                RemoveMoney(healthPrices[healthUpgradesAmount - 1]);
                moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
                healthUpgradesAmount++;
                healthSlider.value = healthUpgradesAmount;
                healthPriceText.text = healthPrices[healthUpgradesAmount - 1].ToString();
                if (healthUpgradesAmount == healthSlider.maxValue)
                {
                    healthPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("HealthUpgrades", healthUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") < healthPrices[healthUpgradesAmount - 1])
                {
                    Debug.Log("You need more money");
                }
            }
        }
        else
        {
            healthPriceText.text = "MAXIMUM LEVEL";
        }
    }
    public void UpgradePowerUps()
    {
        if (powerUpsUpgradesAmount < powerUpsSlider.maxValue)
        {
            if (PlayerPrefs.GetInt("Money") >= powerUpsPrices[powerUpsUpgradesAmount - 1])
            {
                RemoveMoney(powerUpsPrices[powerUpsUpgradesAmount - 1]);
                moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
                powerUpsUpgradesAmount++;
                powerUpsSlider.value = powerUpsUpgradesAmount;
                powerUpsPriceText.text = powerUpsPrices[powerUpsUpgradesAmount - 1].ToString();
                if (powerUpsUpgradesAmount == powerUpsSlider.maxValue)
                {
                    powerUpsPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("PowerUpsUpgrades", powerUpsUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") < powerUpsPrices[powerUpsUpgradesAmount - 1])
                {
                    Debug.Log("You need more money");
                }
            }
        }
        else
        {
            powerUpsPriceText.text = "MAXIMUM LEVEL";
        }
    }
    public void UpgradeBooster()
    {
        if (speedBoosterUpgradesAmount < speedBoosterSlider.maxValue)
        {
            if (PlayerPrefs.GetInt("Money") >= speedBoosterPrices[speedBoosterUpgradesAmount - 1])
            {
                RemoveMoney(speedBoosterPrices[speedBoosterUpgradesAmount - 1]);
                moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
                speedBoosterUpgradesAmount++;
                speedBoosterSlider.value = speedBoosterUpgradesAmount;
                speedBoosterPriceText.text = speedBoosterPrices[speedBoosterUpgradesAmount - 1].ToString();
                if (speedBoosterUpgradesAmount == speedBoosterSlider.maxValue)
                {
                    speedBoosterPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("SpeedBoosterUpgrades", speedBoosterUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") < speedBoosterPrices[speedBoosterUpgradesAmount - 1])
                {
                    Debug.Log("You need more money");
                }
            }
        }
        else
        {
            speedBoosterPriceText.text = "MAXIMUM LEVEL";
        }
    }
    public void UpgradeInventorySize()
    {

    }
    public void GoToMainMenu()
    {
        UpgradesScreen.SetActive(false);
       
        if (!mainMenuScreen.activeSelf)
        {
            mainMenuScreen.SetActive(true);
        }
    }
    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();

        speedUpgradesAmount = 1;
        speedSlider.value = speedUpgradesAmount;
        speedPriceText.text = speedPrices[0].ToString();

        weaponsUpgradesAmount = 1;
        weaponsSlider.value = weaponsUpgradesAmount;
        weaponsPriceText.text = weaponsPrices[0].ToString();
        
        shieldUpgradesAmount = 1;
        shieldSlider.value = shieldUpgradesAmount;
        shieldPriceText.text = shieldPrices[0].ToString();
        
        healthUpgradesAmount = 1;
        healthSlider.value = healthUpgradesAmount;
        healthPriceText.text = healthPrices[0].ToString();

        powerUpsUpgradesAmount = 1;
        powerUpsSlider.value = powerUpsUpgradesAmount;
        powerUpsPriceText.text = powerUpsPrices[0].ToString();

        speedBoosterUpgradesAmount = 1;
        speedBoosterSlider.value = speedBoosterUpgradesAmount;
        speedBoosterPriceText.text = speedBoosterPrices[0].ToString();
       
        PlayerPrefs.SetInt("Money", 0);
        moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();

        PlayerPrefs.Save();
    }
    void RemoveMoney(int amount)
    {
        int money = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", money - amount);

    }
    public void GiveMoney()
    {
        PlayerPrefs.SetInt("Money", 100000);
        moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
    }

}
