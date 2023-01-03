using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUD : MonoBehaviour
{

    public static HUD instance;

    //set total time
    [SerializeField] private float timer = 5f;

    //time count UI
    public Text timerText;

    // Gameover UI
    public Text winloseText;
    public bool isTimeOut = false;
    public bool isDead = false;
    public bool outOfScene = false;

    // Player's score
    public Text scoreText;
    private int playerScore;

    public GameObject gameOverMenu;
    private int winScore = 5; // Score needed to get a win


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
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
            winloseText.text = "You Win";
        }else if (isTimeOut)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true);
            winloseText.text = "Time is Up. You Lose";
        }
        else if (isDead)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true);
            winloseText.text = "You are Dead. You Lose";
        }else if(outOfScene)
        {
            Time.timeScale = 0; // stop all
            gameOverMenu.SetActive(true);
            winloseText.text = "Out of Scene. You Lose";
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
                //Conflicts with the previous timeout detection
                //timerText.text = "00:00";
                //winloseText.text = "Dead...";
                //Invoke("Restart", 2f);
            }
        }
    }


    public void SetScoreText()
    {
        // Update score
        playerScore++;
        scoreText.text = "Score: " + playerScore.ToString();

    }

/*    public void checkState()
    {
        // Player is dead
        isDead = true;
    }*/

/*    public void checkScene()
    {
        // Player is out of scene
        outOfScene = true;
    }*/


    void AlreadyWin()//to avoid wrong win/lose check
    {
        gameObject.SetActive(false);
        Invoke("Restart", 0f);
    }


}
