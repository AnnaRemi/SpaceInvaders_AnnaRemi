using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverScore : MonoBehaviour
{
    public TMP_Text OverScore;
    public TMP_Text BestScore;
    void Update()
    {
        OverScore.text = "SCORE: " + PlayerPrefs.GetInt("Score");
        BestScore.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore");
    }
}
