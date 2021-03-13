using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MoneyTextScript : MonoBehaviour
{
    TMP_Text moneyText;
    private Text text;
    int levelNumber;
    void OnEnable()
    {
        
      
        moneyText = GetComponent<TMP_Text>();
        moneyText.text = PlayerPrefs.GetInt("Money").ToString();
    }
    private void Awake()
    {
        text = transform.Find("text").GetComponent<Text>();
        Text();
    }
    void Text()
    {
        text.text = "SomeText" +levelNumber;
    }
}
