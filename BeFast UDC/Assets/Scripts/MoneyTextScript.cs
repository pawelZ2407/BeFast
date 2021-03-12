using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyTextScript : MonoBehaviour
{
    TMP_Text moneyText;
    void OnEnable()
    {
        moneyText = GetComponent<TMP_Text>();
        moneyText.text = PlayerPrefs.GetInt("Money").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
