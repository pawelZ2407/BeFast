using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;
using Cinemachine;
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
    [SerializeField] int enemiesPerSecond;
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject moneyText;

    public Transform player;
    float shakeTimer;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private void Start()
    {
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        EnemiesPreInstantiate();
    }

    [SerializeField] TMP_Text scoreText;
    public int score;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] List<Transform> coinsSpawnPlaces = new List<Transform>();
    [SerializeField] List<Transform> enemiesSpawnPlaces = new List<Transform>();
    [SerializeField] List<GameObject> enemiesTypesList = new List<GameObject>();

    [SerializeField] List<Transform> powerUpsSpawnPlaces = new List<Transform>();
    [SerializeField] List<GameObject> powerUps = new List<GameObject>();
    public bool isPowerUpSpawned;

    float timer;
    [SerializeField] TMP_Text timerText;

    public Vector2 laserEndPosition;

    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString();
    }
    private void EnemiesPreInstantiate()
    {
    for(int i = 0; i <= 500; i++)
        {
            enemiesToSpawn.Add(Instantiate(enemiesTypesList[0]));
            enemiesToSpawn[i].GetComponent<AIDestinationSetter>().target = player;
        }
        
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        AudioManager.instance.StopSFXSound();
        gameOverScreen.SetActive(true);
        PlayerPrefs.SetInt("Money", score + PlayerPrefs.GetInt("Money"));
        moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
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
    #region coinsSpawn;
    public void SpawnCoin()
    {

        var coin = Instantiate(coinPrefab);
        coin.transform.position = coinsSpawnPlaces[Random.Range(0, coinsSpawnPlaces.Count - 1)].position;
        SpawnEnemies(coin.transform.position, (int)timer);
        if (!isPowerUpSpawned)
        {
            SpawnPowerUps();
        }

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
        if (enemiesToSpawn.Count - 1 >= amountOfEnemies * enemiesPerSecond)
        {
            for (int j = 0; j < amountOfEnemies * enemiesPerSecond; j++)
            {
                if (!enemiesToSpawn[j].activeSelf)
                {
                    enemiesToSpawn[j].SetActive(true);
                    enemiesToSpawn[j].transform.position = closestSpawner;
                    enemiesToSpawn[j].GetComponent<AIDestinationSetter>().target = player;
                }
            }
        }
        else
        {
            for (int j = 0; j < enemiesToSpawn.Count; j++)
            {
                if (!enemiesToSpawn[j].activeSelf)
                {
                    enemiesToSpawn[j].SetActive(true);
                    enemiesToSpawn[j].transform.position = closestSpawner;

                }
            }
        }
       
        
    }
    void SpawnPowerUps()
    {
        Vector2 randomPos = powerUpsSpawnPlaces[Random.Range(0, powerUpsSpawnPlaces.Count)].position;
        Instantiate(powerUps[Random.Range(0, powerUps.Count)], randomPos,Quaternion.identity);
        isPowerUpSpawned = true;
    }
    #endregion;

    public void ShakeCamera(float amplitudeGain, float time)
    {     
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitudeGain;
        shakeTimer = time;
        StartCoroutine(CameraShakeCoroutine());
    }
    IEnumerator CameraShakeCoroutine()
    {
        yield return new WaitForSeconds(shakeTimer);      
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }

}
