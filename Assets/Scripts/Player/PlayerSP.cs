using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSP : MonoBehaviour
{
    public int maxSP = 100; // Initial SP
    public int currentSP;

    private PlayerController playerController;
    private GameObject player;

    public int consumeSP = 10; // Cost 10 SP/s
    public int restoreSP = 20; // Restore 20 SP/s

    public float timeBetweenConsume = 1f;

    public Image spBar;

    public static PlayerSP instance;

    private void Awake()
    {
        instance = this;
        currentSP = maxSP;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }


    private void Update()
    {
        // Change spBar display
        if (currentSP <= 0)
        {
            spBar.fillAmount = 0;
        }
        else
        {
            spBar.fillAmount = (float)currentSP / (float)maxSP;
        }
    }


    public void ConsumSP()
    {
        currentSP = currentSP - consumeSP;

        if (currentSP < 5)
        {
            playerController.canSpeedUp = false;  // Stop speed up

        }
        else
        {
            playerController.canSpeedUp = true;  // Speed up
        }
        
    }


    public void RestoreSP()
    {
        playerController.canSpeedUp = true;

        if (currentSP < 90)
        {
            currentSP = currentSP + restoreSP;   // SP restore
        }
        else
        {
            currentSP = 100;
        }


    }



    public int getSP()
    {
        return currentSP;
    }

    public void setSP(int saveDate)
    {
        currentSP = saveDate;
    }
}
