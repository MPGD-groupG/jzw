using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public GameObject gamePauseMenu;
    public GameObject guidePanel;
    public GameObject settingPanel;
    public bool isPause;

    void Start()
    {
        instance = this;
        gamePauseMenu.SetActive(false);
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause || Time.timeScale == 0)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
        }

    }

    public void OnPause()
    {
        Time.timeScale = 0;
        gamePauseMenu.SetActive(true);
        guidePanel.SetActive(false);
        settingPanel.SetActive(false);
        isPause = false;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        gamePauseMenu.SetActive(false);
        guidePanel.SetActive(false);
        settingPanel.SetActive(false);

    }

}
