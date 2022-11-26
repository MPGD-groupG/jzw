using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public GameObject gamePauseMenu;
    public bool isPause;

    void Start()
    {
        instance = this;
        gamePauseMenu.SetActive(false);
    }


    void Update()
    {

/*        if (Input.GetKeyDown(KeyCode.Escape) && Pause) // Pause the game and awake game menu
        {
            Time.timeScale = 0;
            gamePauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !Pause)
        {
            Time.timeScale = 1;
            gamePauseMenu.SetActive(false);
        }*/

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
        isPause = false;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        gamePauseMenu.SetActive(false);
        
    }

}
