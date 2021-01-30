using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Absolutely abusing this for the jam
    public static int score;
    public static int health;
    public const int MAX_HEALTH = 4;
    public static int weaponLevel = 1;
    public static float weaponExp;
    public const float EXP_TO_LEVEL = 10; // leveling up takes 10 exp
    public const float TIME_TILL_EXP_DRAIN = 2; // lose 1 every 2 seconds
    public const float EXP_DRAIN_WAIT_LVLUP = 4;
    public const float INVUL_TIME = 2;
    public static float ExpLostOnHit
    {
        get
        {
            return Mathf.Floor(weaponExp/4);
        }
    }
}
