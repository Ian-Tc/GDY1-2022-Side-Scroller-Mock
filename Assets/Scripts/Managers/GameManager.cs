using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    public enum GameStates
    {
        LEVELINTRO,  //Entry into the level
        GAMEPLAY,    //Game is running
        GAMEPAUSE,   //We pause the game
        LEVELOUTRO,  //Victory Condition of Level
        PCDEATH,     //This is what happens when I lose Hitpoints
        GAMEOVER     //This is when all lives are over
    }

    public static GameManager instance;
    public GameStates gStates;

    //Game UI
    public GameObject pauseMenu;

    public PlayableDirector timelineDirector;
    bool subscibeCheck;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }

        gStates = GameStates.LEVELINTRO;
        
        StartTimeLine();
        subscibeCheck = true;
        timelineDirector.stopped += EndTimeLine;
        GameUnpause();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gStates)
        {
            case GameStates.LEVELINTRO:
                break;

            case GameStates.GAMEPLAY:
                if (subscibeCheck)
                {
                    subscibeCheck = false;
                    timelineDirector.stopped -= EndTimeLine;
                }

                CheckForPauseInput();
                break;

            case GameStates.GAMEPAUSE:
                GamePause();
                break;

            case GameStates.LEVELOUTRO:
                Time.timeScale = 0f;
                LoadNextScene();
                break;

            case GameStates.PCDEATH:
                LoadRequiredScene(SceneManager.GetActiveScene().buildIndex);
                break;

            case GameStates.GAMEOVER:
                LoadRequiredScene(0);
                break;
        }
    }

    void CheckForPauseInput()
    {
        if (Input.GetButtonDown("Pause"))
        {
            gStates = GameStates.GAMEPAUSE;
        }
    }

    void GamePause()
    {
        EnableMenuPanel(pauseMenu);
        Time.timeScale = 0f;
    }

    void GameUnpause()
    {
        DisableMenuPanel(pauseMenu);
        Time.timeScale = 1f;
    }

    void EnableMenuPanel(GameObject panelName)
    {
        panelName.SetActive(true);
    }

    void DisableMenuPanel(GameObject panelName)
    {
        panelName.SetActive(false);
    }

    void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            LoadRequiredScene(0);
        }
       
    }

    void LoadRequiredScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void PauseMenuButton(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 1:
                Debug.Log("Resume Button");
                GameUnpause();
                gStates = GameStates.GAMEPLAY;
                break;

            case 2:
                Debug.Log("Options Button");
                break;

            case 3:
                Debug.Log("Restart Button");
                gStates = GameStates.PCDEATH;
                break;

            case 4:
                Debug.Log("Main Menu Button");
                gStates = GameStates.GAMEOVER;
                break;

            default:
                break;
        }
    }

    public void DisableGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void StartTimeLine()
    {
        timelineDirector.Play();
    }

    public void EndTimeLine(PlayableDirector obj)
    {
        gStates = GameStates.GAMEPLAY;
    }
}
