using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Pretending this is a singleton but it can be easily broken
    public static GameManager gameManager;
    public AudioSource audioSource;
    public RawImage fog;
    public GameObject gameOver;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI gameOverHighScoreText;
    public TMPro.TextMeshProUGUI gameOverScoreText;

    public bool isDead = false;

    public GameObject explosionPrefab;

    // Absolutely abusing this for the jam
    public static int score;
    public static int health;
    public  int maxHealth = 4;
    private Color fogColor = Color.white;
    private float gameStartTime;

    public float currentSpeed = 1;
    public float fogTransitionDelay = 10;
    public float fogTransitionTime = 30;
    public const int maxDigits = 8;

    // Will add stuff for these if there is time
    public static int weaponLevel = 1;
    public static float weaponExp;
    public  float expToPowerUp = 10; // leveling up takes 10 exp
    public  float TIME_TILL_EXP_DRAIN = 2; // lose 1 every 2 seconds
    public  float EXP_DRAIN_WAIT_LVLUP = 4;
    public  float INVUL_TIME = 2;
    public float speedUpValue = 120; // addition + 1 speed every 2 minutes if 120
    public float deadCLickDelay;
    public float deathTime;

    public const int GAME_WIDTH = 384;
    public const int GAME_HEIGHT = 216;
    public static float ExpLostOnHit
    {
        get
        {
            return Mathf.Floor(weaponExp/4);
        }
    }

    public static void IncreaseScore(int points)
    {
        score += points;
        if(score > 99999999) // doubt this will ever happen
        {
            score = 99999999;
        }
        // Don't know how to save in webgl so abusing prefs - don't judge me
        if(PlayerPrefs.GetInt("HighScore") < points)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        var scoreValue = score.ToString();
        for (int i = scoreValue.Length; i < GameManager.maxDigits; i++)
        {
            scoreValue = $"0{scoreValue}";
        }

        gameManager.scoreText.text = $"Score : {scoreValue}";
        gameManager.gameOverHighScoreText.text = $"Score : {scoreValue}";

        var highScoreValue = Mathf.Max(PlayerPrefs.GetInt("HighScore")).ToString();
        for (int i = highScoreValue.Length; i < GameManager.maxDigits; i++)
        {
            highScoreValue = $"0{highScoreValue}";
        }
        gameManager.gameOverHighScoreText.text = $"HighScore : {highScoreValue}";
    }

    private void Start()
    {
        GameManager.gameManager = this;
        gameStartTime = Time.time;
        
    }

    private void Update()
    {

        if (isDead)
        {
            Time.timeScale = Mathf.Max(Time.timeScale - 0.5f * Time.unscaledDeltaTime, 0);
            audioSource.pitch = Mathf.Max(audioSource.pitch - 0.25f * Time.unscaledDeltaTime, 0);

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(1);
            }
            return;
        }

        Cursor.visible = false;
        fogColor.a = Mathf.Min(1, (Time.time - gameStartTime - fogTransitionDelay) / fogTransitionTime);
        var multiplierOffset = Mathf.Clamp((Time.time - gameStartTime - 40f) / speedUpValue, 0, 2);
        Time.timeScale = 1 + multiplierOffset;
        audioSource.pitch = 1 + multiplierOffset / 2;
        fog.color = fogColor;
    }

    public void EndGame()
    {
        isDead = true;
        deathTime = Time.time;
        gameOver.SetActive(true);
    }
}
