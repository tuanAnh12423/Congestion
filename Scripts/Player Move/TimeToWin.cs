using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeToWin : MonoBehaviour
{
    public float gameDurationInSeconds = 300f; // Thời gian chơi tính bằng giây (5 phút = 300 giây)
    public TMP_Text timerText; // Reference đến UI Text để hiển thị đồng hồ đếm ngược
    public int pointsPerExtraSecond = 100; // Số điểm cộng thêm cho mỗi giây còn dư
    public TMP_Text highScoreText; // Reference đến UI Text để hiển thị điểm số cao nhất

    private float currentTime;
    private bool isGameActive = true;

    void Start()
    {
        currentTime = gameDurationInSeconds;
        UpdateTimerUI();
        DisplayHighScore();
    }

    void Update()
    {
        if (isGameActive)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                EndGame();
            }

            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        isGameActive = false;
        Time.timeScale = 0f; // Dừng thời gian trong game

        int extraSeconds = Mathf.FloorToInt(gameDurationInSeconds - currentTime);
        int extraPoints = extraSeconds * pointsPerExtraSecond;

        if (extraPoints > 0)
        {
            int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

            if (extraPoints > currentHighScore)
            {
                PlayerPrefs.SetInt("HighScore", extraPoints);
                PlayerPrefs.Save();
                DisplayHighScore();
            }
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        Debug.Log("Kết thúc màn chơi!");
    }

    void DisplayHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
