using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance!=null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        SpawnCoin();
        AddScore(0);
        SavingSystemInit();
       
    }

    [SerializeField] TMP_Text text;
    public int score;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] List<Transform> coinsSpawnPlaces = new List<Transform>();

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        text.text = score.ToString();
    }
    public void GameOver()
    {
        PlayerPrefs.SetInt("Money", score + PlayerPrefs.GetInt("Money"));
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        PlayerPrefs.Save();
    }
    private void SavingSystemInit()
    {
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", score);
        }
    }

    public void SpawnCoin()
    {
        var coin = Instantiate(coinPrefab);
        coin.transform.position = coinsSpawnPlaces[Random.Range(0, coinsSpawnPlaces.Count - 1)].position;
    }
}
