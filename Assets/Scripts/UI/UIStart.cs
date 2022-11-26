using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIStart : MonoBehaviour
{
    public GameObject gameStartMenu;
    public GameObject screenGuide;


    void Start()
    {

    }


    public void OnNewGameClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnResumeClicked()
    {

    }


    public void OnGuideClicked()
    {
        screenGuide.SetActive(true);
    }


    public void OnQuitClicked()
    {
        Application.Quit();
    }

}
