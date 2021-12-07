using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text gameTimesText;
    public Text highScoreText;
    public Text overallPointsText;
    public Text userNameText;
    public Button playButton;
    public Button quitButton;

    public void Start()
    {
        playButton = GetComponent<Button>();
        quitButton = GetComponent<Button>();
        gameTimesText.text = "";
        highScoreText.text = "";
        userNameText.text = "";
        overallPointsText.text = "";
        GetPlayerData();
    }

    public void GetPlayerData()
    {
        string id = PlayerPrefs.GetString("playerId");
        string token = PlayerPrefs.GetString("playerToken");
        string url = String.Format("https://project-game-api.herokuapp.com/api/users/" + id);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        request.ContentType = "application/json";
        request.Method = "GET";

        try
        {
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();

                string playerName;
                string gameTimes;
                string highScore;
                string overallPoints;

                string[] dataParts = result.Split(':', ','); ;

                playerName = dataParts[3].Trim('"');
                gameTimes = dataParts[7];
                highScore = dataParts[9];
                overallPoints = dataParts[11];

                userNameText.text = playerName + "`s stats:";
                gameTimesText.text = "Play times: " + gameTimes;
                highScoreText.text = "High score: " + highScore;
                overallPointsText.text = "Total points: " + overallPoints;

                PlayerPrefs.SetString("PlayerName", playerName);
                PlayerPrefs.SetString("GameTimes", gameTimes);
                PlayerPrefs.SetString("HighScore", highScore);
                PlayerPrefs.SetString("OverallPoints", overallPoints);
                PlayerPrefs.Save();

            }
        }
        catch (WebException error)
        {
            Debug.Log(error);
            return;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
