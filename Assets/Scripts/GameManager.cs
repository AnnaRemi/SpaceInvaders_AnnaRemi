using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    public TMP_Text LivesText;
    private int lives = 5;

    void Start()
    {
        UpdateScore(0);
        UpdateLives(0);
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        PlayerPrefs.SetInt("Score", score);
        if (score > PlayerPrefs.GetInt("BestScore"))
            PlayerPrefs.SetInt("BestScore", score);
        scoreText.text = score + " POINTS";
    }

    public void UpdateLives(int livesTaken)
    {
        lives -= livesTaken;
        if (lives == 0)
        {
            KillPlayer();
        }
        LivesText.text = "LIVES: " + lives;
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene("YouLost");
    }

    public void PlayerWon()
    {
        SceneManager.LoadScene("YouWon");
    }
}
