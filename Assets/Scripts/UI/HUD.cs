using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class HUD : MonoBehaviour
{

    public static HUD instance;

    //set total time
    [SerializeField] private float timer = 5f;

    //time count UI
    public Text timerText;
    public float timeBetweenUpdate = 10f;
    private float time;
    private float allTime;               // Total time played
    public int min;                      // Total minutes played

    // Gameover UI
    public Text winloseText;
    public bool isTimeOut = false;
    public bool isDead = false;
    public bool outOfScene = false;

    // Player's score
    public Text scoreText;
    private int playerScore;

    public GameObject gameOverMenu;
    public int winScore = 50; // Score needed to get a win

    // Modes
    public Toggle modeToggle;
    public Text modeText;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        modeToggle.isOn = true;
        gameOverMenu.SetActive(false); // Don't display game over menu at first
    }

    void Update()
    {
        Timer();

        // Can use switch
        if (playerScore >= winScore)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true); // Display the game over menu
            winloseText.text = "Congratulations! You Win";
        }else if (isTimeOut)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true);
            winloseText.text = "Time is Up. You Lose...";
        }
        else if (isDead)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true);
            winloseText.text = "You are Dead. You Lose...";
        }else if(outOfScene)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true);
            winloseText.text = "Out of Scene. You Lose...";
        }

        if (modeToggle.isOn == false)
        {
            winScore = 100;
            modeText.text = "Now in hard mode";
            modeText.color = Color.red;

        }
        else
        {
            winScore = 50;
            modeText.text = "Now in easy mode";
            modeText.color = Color.green;
        }

    }

    private void FixedUpdate()
    {
        allTime = allTime + Time.deltaTime;
        min = (int)(allTime) / 60;

        time = time + Time.deltaTime;
        if (time >= timeBetweenUpdate)
        {
            time = 0;
            playerScore = playerScore + (int)(Math.Pow(2, min));  // Time bonus increases as play time grows
            scoreText.text = "Score: " + playerScore.ToString();
        }
    }

    public void Restart()
    {
        // Reset game
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        // Quit game
        Application.Quit();

    }


    private void Timer()
    {
        if(isTimeOut == false)
        {
            timer -= Time.deltaTime;
            
            timerText.text = timer.ToString("F2");

            if(timer <=0)
            {
                isTimeOut = true;
            }
        }
    }


    public void SetScoreText(int score)
    {
        // Update score
        playerScore += score;
        scoreText.text = "Score: " + playerScore.ToString();

    }


}
