using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    //set total time
    [SerializeField] private float timer = 5f;

    //time count UI
    public Text timerText;

    //win UI
    public Text winloseText;  
    private bool isTimeOut = false;

    public Text scoreText;
    private int playerScore;

    public GameObject gameOverMenu;
    private int winScore = 3; // 赢的分数


    private void Awake()
    {
        instance = this;
        gameOverMenu.SetActive(false); // 开始时菜单不显示
    }

    void Update()
    {
        Timer();

        if (playerScore >= winScore)
        {
            //Time.timeScale = 0; // stop all，bug是重新载入的时候玩家动不了了
            //gameObject.SetActive(false); // 但这样的话 游戏结束时怪物还在动
            gameOverMenu.SetActive(true); // Display the gameover menu
            winloseText.text = "You win";
        }else if (isTimeOut)
        {
            //Time.timeScale = 0; // stop all
            //gameObject.SetActive(false);
            gameOverMenu.SetActive(true);
            winloseText.text = "You lose";
        }

    }

    public void Restart()
    {
        //reset game when win
        SceneManager.LoadScene("SampleScene");
        //Time.timeScale = 1f;
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
                timerText.text = "00:00";
                winloseText.text = "Dead...";
                //Invoke("Restart", 2f);
            }
        }
    }


    public void SetScoreText()
    {
        //win check
        // Change score
        playerScore++;
        scoreText.text = "Score: " + playerScore.ToString();

    }

    void AlreadyWin()//to avoid wrong win/lose check
    {
        gameObject.SetActive(false);
        Invoke("Restart", 0f);
    }


}
