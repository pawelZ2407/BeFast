using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradesDescriptions : MonoBehaviour
{

    [SerializeField] TMP_Text descriptionTextUI;
    readonly string speedDescription = "Be FASTER";
    readonly string weaponsDescription = "Increase rate of fire and deal more damage for balls, kill faster and deal more AoE laser damage ";
    readonly string shieldDescription = "Increase shield recovery speed and shield limit";
    readonly string healthDescription = "Increase maximum HP";
    readonly string powerUpsDescription = "Increase force field duration";
    readonly string boosterDescription = "Decrease fuel usage, increase fuel recover";
    public void ShowSpeedDesc()
    {
        descriptionTextUI.text = speedDescription;
        descriptionTextUI.gameObject.SetActive(true);
    }
    public void ShowWeaponsDesc()
    {
        descriptionTextUI.text = weaponsDescription;
        descriptionTextUI.gameObject.SetActive(true);
    }
    public void ShowShieldDesc()
    {
        descriptionTextUI.text = shieldDescription;
        descriptionTextUI.gameObject.SetActive(true);
    }
    public void ShowHealthDesc()
    {
        descriptionTextUI.text = healthDescription;
        descriptionTextUI.gameObject.SetActive(true);
    }
    public void ShowPowerUpsDesc()
    {
        descriptionTextUI.text = powerUpsDescription;
        descriptionTextUI.gameObject.SetActive(true);
    }
    public void ShowBoosterDesc()
    {
        descriptionTextUI.text = boosterDescription;
        descriptionTextUI.gameObject.SetActive(true);
    }
}
