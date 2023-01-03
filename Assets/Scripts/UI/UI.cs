using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public StateManager stateManager;
    public GameObject gamePauseMenu;
    public GameObject gameOverMenu;

    public GameObject screenGuide;
    public GameObject screenSetting;

    private int currentScreen;
    private bool inGame;
    public Text resumeText;
    private int screenType;
    public GameObject ttl;

    public static UI instance;

    void Start()
    {
        instance = this;
    }


    public void OnPlayClicked()
    {
        gamePauseMenu.SetActive(false);
        inGame = true;
        resumeText.color = new Color(50f / 256f, 50f / 256f, 50f / 256f);
        Time.timeScale = 1f;
    }

    public void OnContinueClicked()
    {
        Time.timeScale = 1f;
        gamePauseMenu.SetActive(false);

    }

    public void OnReplayClicked()
    {
        gamePauseMenu.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }


    public void OnGuideClicked()
    {
        screenGuide.SetActive(true);
    }


    public void OnSettingClicked()
    {
        screenSetting.SetActive(true);
    }


    public void OnCloseMenuClicked()
    {
        gamePauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


    public void OnQuitClicked()
    {
        Application.Quit();
    }

}
