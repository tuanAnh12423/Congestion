using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;
    [SerializeField] private AudioSource deadSoundEffect;

    private bool isPaused = false;
    public void GameOver()
    {
        if (!gameOverUI.activeSelf)
        {
            Time.timeScale = 0f; // Pause the game
            gameOverUI.SetActive(true); // show the game over UI
        }
    }
    public void RestartGame() // load scene
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;  // pause the game 
        }
    }
    public void ReturnToGame() // return after pause
    {
        Time.timeScale = 1f;
    }
    public virtual void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
