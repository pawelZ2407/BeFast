using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;
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
    public Transform player;
    [SerializeField] TMP_Text text;
    public int score;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] List<Transform> coinsSpawnPlacesDefault = new List<Transform>();
    [SerializeField] List<Transform> coinsSpawnPlaces = new List<Transform>();
    [SerializeField] List<Transform> enemiesSpawnPlaces = new List<Transform>();
    [SerializeField] List<Transform> powerUpsSpawnPlaces = new List<Transform>();
    [SerializeField] List<GameObject> enemiesList = new List<GameObject>();

    float timer;
    [SerializeField] TMP_Text timerText;
    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString();
    }
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
        SpawnEnemies(coin.transform.position, (int)timer);
        timer = 0;
    }
    public void SpawnEnemies(Vector2 coinPosition, int amountOfEnemies)
    {
        float closestDistance=10000;
        Vector2 closestSpawner = enemiesSpawnPlaces[0].position;
        for(int i = 0; i <= enemiesSpawnPlaces.Count - 1; i++)
        {
            if (Vector2.Distance(enemiesSpawnPlaces[i].position, coinPosition) < closestDistance){
                closestDistance = Vector2.Distance(enemiesSpawnPlaces[i].position, coinPosition);
                closestSpawner = enemiesSpawnPlaces[i].position;
            }

        }
        for(int j=0; j < amountOfEnemies; j++)
        {
            var enemy = Instantiate(enemiesList[Random.Range(0, enemiesList.Count - 1)]);
            enemy.transform.position = closestSpawner;
            enemy.GetComponent<AIDestinationSetter>().target = player;
        }
        
    }
    void SpawnPowerUps()
    {

    }
}
