using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    //set total time
    [SerializeField] private float timer = 5f;

    //time count UI
    public Text timerText;
    //win UI
    public Text winloseText;  
    private bool isTimeOut = false;

    void Update()
    {
        Timer();
    }

    void Restart()
    {
        //reset game when win
        SceneManager.LoadScene(0);
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
                Invoke("Restart", 2f);
            }
        }
    }
}
