using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public static int points = 0;
    public GameOverMenu gameOverMenu;

    public Text pointsCounter;

    public void Start()
    {
        pointsCounter.text = "Points: " + points.ToString();
    }

    public void GameOver()
    {
        gameOverMenu.Setup(points);        
    }

    public void AddPoint(int point)
    {
        string score;
        score = (points += point).ToString();
        pointsCounter.text = "Points: " + score.ToString();
    }
}
