using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSP : MonoBehaviour
{
    private PlayerController playerController;
    public static PlayerSP instance;
    private GameObject player;
    public Image spBar;

    public float maxSP = 100; // Initial SP
    public float currentSP;

    public float consumeSP = 5; // Cost 5 SP/s
    public float autoRestoreSP = 5; // Restore 5 SP/s
    public float itemRestoreSP; // Restore x SP/s

    public float timeBetweenConsume = 1f;


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

        if (itemRestoreSP != 0)
        {
            currentSP += itemRestoreSP;
            itemRestoreSP = 0;
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
            currentSP = currentSP + autoRestoreSP;   // SP restore
        }
        else
        {
            currentSP = 100;
        }


    }


}
