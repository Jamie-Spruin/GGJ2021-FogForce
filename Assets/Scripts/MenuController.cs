using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highScore;

    private void Start()
    {
        // Don't know how to save in webgl so abusing prefs - don't judge me
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 15000);
        }
        var highScoreValue = Mathf.Max(PlayerPrefs.GetInt("HighScore", 0)).ToString();
        for (int i = highScoreValue.Length; i < GameManager.maxDigits; i++)
        {
            highScoreValue = $"0{highScoreValue}";
        }
        highScore.text = $"HighScore : {highScoreValue}";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}
