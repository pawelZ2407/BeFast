using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradesController : MonoBehaviour
{
    [SerializeField] GameObject UpgradesScreen;
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] Slider speedSlider;
    [SerializeField] TMP_Text speedPriceText;
    int speedUpgradesAmount = 1;
    int[] speedPrices = new int[15] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000 };

    [SerializeField] Slider weaponsSlider;
    [SerializeField] TMP_Text weaponsPriceText;
    int weaponsUpgradesAmount = 1;
    int[] weaponsPrices = new int[5] { 1000,2000,3000,4000,5000 };
    
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

    private void Awake()
    {
        Initializer();
    }

    private void Start()
    {

        speedSlider.value = speedUpgradesAmount;
        speedPriceText.text = speedPrices[speedUpgradesAmount - 1].ToString();

        weaponsSlider.value = weaponsUpgradesAmount;
        weaponsPriceText.text = weaponsPrices[weaponsUpgradesAmount - 1].ToString(); 

        shieldSlider.value = shieldUpgradesAmount;
        shieldPriceText.text = shieldPrices[shieldUpgradesAmount - 1].ToString();

        healthSlider.value = healthUpgradesAmount;
        healthPriceText.text = healthPrices[healthUpgradesAmount - 1].ToString();

        powerUpsSlider.value = powerUpsUpgradesAmount;
        powerUpsPriceText.text = powerUpsPrices[powerUpsUpgradesAmount - 1].ToString();

        speedBoosterSlider.value = speedBoosterUpgradesAmount;
        speedBoosterPriceText.text = speedBoosterPrices[speedBoosterUpgradesAmount - 1].ToString();
        PlayerPrefs.SetInt("Money", 10000000);

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
            if (PlayerPrefs.GetInt("Money") >= speedPrices[speedUpgradesAmount])
            {
                PlayerPrefs.SetFloat("Speed", 1 * speedUpgradesAmount);
                RemoveMoney(speedPrices[speedUpgradesAmount]);
                speedUpgradesAmount++;
                speedSlider.value = speedUpgradesAmount;
                speedPriceText.text = speedPrices[speedUpgradesAmount - 1].ToString();
                if (speedUpgradesAmount == speedSlider.maxValue)
                {
                    speedPriceText.text = "MAXIMUM LEVEL";
                }
                PlayerPrefs.SetInt("SpeedUpgrades", speedUpgradesAmount);
                PlayerPrefs.Save();
            }

            else
            {
                if (PlayerPrefs.GetInt("Money") <= speedPrices[speedUpgradesAmount])
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
            if (PlayerPrefs.GetInt("Money") >= weaponsPrices[weaponsUpgradesAmount])
            {
                PlayerPrefs.SetFloat("Weapons", 1 * weaponsUpgradesAmount);
                RemoveMoney(weaponsPrices[weaponsUpgradesAmount]);
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
                if (PlayerPrefs.GetInt("Money") <= weaponsPrices[weaponsUpgradesAmount])
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
            if (PlayerPrefs.GetInt("Money") >= shieldPrices[shieldUpgradesAmount])
            {
                PlayerPrefs.SetFloat("Shield", 1 * shieldUpgradesAmount);
                RemoveMoney(shieldPrices[shieldUpgradesAmount]);
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
                if (PlayerPrefs.GetInt("Money") <= shieldPrices[shieldUpgradesAmount])
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
            if (PlayerPrefs.GetInt("Money") >= healthPrices[healthUpgradesAmount])
            {
                PlayerPrefs.SetFloat("Health", 1 * healthUpgradesAmount);
                RemoveMoney(healthPrices[healthUpgradesAmount]);
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
                if (PlayerPrefs.GetInt("Money") <= healthPrices[healthUpgradesAmount])
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
            if (PlayerPrefs.GetInt("Money") >= powerUpsPrices[powerUpsUpgradesAmount])
            {
                PlayerPrefs.SetFloat("PowerUps", 1 * powerUpsUpgradesAmount);
                RemoveMoney(powerUpsPrices[powerUpsUpgradesAmount]);
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
                if (PlayerPrefs.GetInt("Money") <= powerUpsPrices[powerUpsUpgradesAmount])
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
            if (PlayerPrefs.GetInt("Money") >= speedBoosterPrices[speedBoosterUpgradesAmount])
            {
                PlayerPrefs.SetFloat("SpeedBooster", 1 * speedBoosterUpgradesAmount);
                RemoveMoney(speedBoosterPrices[speedBoosterUpgradesAmount]);
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
                if (PlayerPrefs.GetInt("Money") <= speedBoosterPrices[speedBoosterUpgradesAmount])
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
        PlayerPrefs.SetInt("Money", 1000000);

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
        PlayerPrefs.Save();
    }
    void RemoveMoney(int amount)
    {
        int money = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", money - amount);
    }
}
