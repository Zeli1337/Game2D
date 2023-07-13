using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_ : MonoBehaviour
{
    // pause muss aus sein. 
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;

    

    private ScenePersist scenepersist;

    public GameObject muteButton;
    public  GameObject unmuteButton;

    public GameObject music;


    private void Awake()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        
        


    }

    public bool getStatus()
    {
        return isGamePaused;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    public void quitGame()
    {
        //FindObjectOfType<GameSession>().saveSession(); Nicht nötig
        Application.Quit();
    }


    public void mute()
    {
        music.SetActive(false);
        unmuteButton.SetActive(true);
        muteButton.SetActive(false);
        
        
    }

    public void unmute()
    {
        music.SetActive(true);
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
    }

    public void replay()
    {
        FindObjectOfType<GameSession>().ResetGameSessionMenu();
        SaveSystem.deleteSaveGame();
        SceneManager.LoadScene(0);
        
    }

    public void Menu()
    {
        //FindObjectOfType<GameSession>().saveSession(); nicht nötig
        FindObjectOfType<GameSession>().ResetGameSessionMenu();
        SceneManager.LoadScene(0);
        
    }
}
