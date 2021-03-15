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
    [SerializeField] TMP_Text gameOverScoreText;

    public Transform player;
    float shakeTimer;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private void Start()
    {
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        EnemiesPreInstantiate();

        if(PlayerPrefs.GetInt("SpeedUpgrades") + PlayerPrefs.GetInt("WeaponsUpgrades") + PlayerPrefs.GetInt("SpeedBoosterUpgrades") >= 10)
        {
            enemiesPerSecond += 1;
        }
    }
    [SerializeField] TMP_Text highscoreText;
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
        float timerAfterRound = (float)Mathf.Round(timer * 100f) / 100f;
        timerText.text = timerAfterRound.ToString();

    }
    private void EnemiesPreInstantiate()
    {
    for(int i = 0; i <= 300; i++)
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
        AudioManager.instance.PlayGameOverSound();
        gameOverScreen.SetActive(true);
        PlayerPrefs.SetInt("Money", score + PlayerPrefs.GetInt("Money"));
        moneyText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
        gameOverScoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        PlayerPrefs.Save();
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
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
        SpawnEnemies(coin.transform.position, Mathf.RoundToInt(timer));
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
        int j = 0;
        for (int i = 0; i <= enemiesToSpawn.Count - 1; i++)
        { 
                if (!enemiesToSpawn[i].activeSelf)
                {
                    enemiesToSpawn[i].SetActive(true);
                    enemiesToSpawn[i].transform.position = closestSpawner;
                    j++;
                }
                if (j >= amountOfEnemies * enemiesPerSecond)
                {
                    break;
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
