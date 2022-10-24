using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HUD : MonoBehaviour
{

    //public GameOver gameOver;

/*    public Text scoreSubText;   // Score text
    public Text numberEnemySubText;  // Enemy number text*/
    public Text numberPropSubText;  // Item number text

    private int initialScore = 0;
    public int currentScore = 0;
    //public int itemNumber = 0;

    public float timeBetweenUpdate = 10f;
    private float time;
    private float allTime;               // Total time played
    public int min;                      // Total minutes played

    public static HUD instance;

    private void Start()
    {
        //OnGameOver();
    }

    private void Awake()
    {
        instance = this;
    }


    public void SetPropsNumber(int propsNumber)
    {
        numberPropSubText.text = propsNumber.ToString();
    }


/*    public void OnGameOver()
    {
        Time.timeScale = 0;
        if (currentScore > 50)
        {
            gameOver.ShowWin(currentScore);
        }
        else
            gameOver.ShowLose(currentScore);

    }*/

/*    private void FixedUpdate()
    {
        allTime = allTime + Time.deltaTime;
        min = (int)(allTime) / 60;

        time = time + Time.deltaTime;
        if (time >= timeBetweenUpdate)
        {
            time = 0;
            currentScore = currentScore + (int)(Math.Pow(2, min));  // Time bonus increases as play time grows
            scoreSubText.text = currentScore.ToString();
        }
    }*/

/*    public int getScore()
    {
        return currentScore;
    }

    public void setSavedScore(int saveDate)
    {
        currentScore = saveDate;
        scoreSubText.text = saveDate.ToString();
    }*/

}
