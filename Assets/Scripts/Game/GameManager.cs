using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Pretending this is a singleton but it can be easily broken
    public static GameManager gameManager;
    public AudioSource audioSource;
    public RawImage fog;

    // Absolutely abusing this for the jam
    public static int score;
    public static int health;
    public  int maxHealth = 4;
    private Color fogColor = Color.white;
    private float gameStartTime;

    public float currentSpeed = 1;
    public float fogTransitionDelay = 10;
    public float fogTransitionTime = 20;

    // Will add stuff for these if there is time
    public static int weaponLevel = 1;
    public static float weaponExp;
    public  float expToPowerUp = 10; // leveling up takes 10 exp
    public  float TIME_TILL_EXP_DRAIN = 2; // lose 1 every 2 seconds
    public  float EXP_DRAIN_WAIT_LVLUP = 4;
    public  float INVUL_TIME = 2;

    public const int GAME_WIDTH = 384;
    public const int GAME_HEIGHT = 216;
    public static float ExpLostOnHit
    {
        get
        {
            return Mathf.Floor(weaponExp/4);
        }
    }

    private void Start()
    {
        GameManager.gameManager = this;
        gameStartTime = Time.time;
    }

    private void Update()
    {
        fogColor.a = Mathf.Min(1, (Time.time - gameStartTime - fogTransitionDelay) / fogTransitionTime);
        fog.color = fogColor;
    }
}
