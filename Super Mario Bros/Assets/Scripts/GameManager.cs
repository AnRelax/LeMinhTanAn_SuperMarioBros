using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    public int scores { get; set; }
    public static int diem = 0;
    public static int dongCoin = 0;
    public static int life = 0;
    public static int worldNow = 1;
    public static int stageNow = 1;
    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 180;
        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        life = lives;
        coins = 0;
        dongCoin = coins;
        scores = 0;
        diem = scores;
        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        // TODO: show game over screen

        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        worldNow = world;
        stageNow = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
        life = lives;
    }

    public void AddCoin()
    {
        coins++;

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
        dongCoin = coins;
    }
    public void AddScoresCoin(){
        scores += 100;
        diem = scores;
    }
    public void AddScoresGoomba(){
        scores += 200;
        diem = scores;
    }
    public void AddScoresFlagPole(){
        scores += 1000;
        diem = scores;
    }
    public void AddScoresKoopa(){
        scores += 300;
        diem = scores;
    }
    public void AddScoresPowerUp(){
        scores += 500;
        diem = scores;
    }

    public void AddLife()
    {
        lives++;
        life = lives;
    }
}
