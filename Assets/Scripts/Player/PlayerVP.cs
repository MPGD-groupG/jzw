using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVP : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHP playerHP;
    public static PlayerVP instance;
    private GameObject player;
    public Image vpBar;


    public float maxVP = 100; // Initial VP
    public float currentVP;

    public float consumeVP = 0.5f; // Cost 0.5 SP/s

    // Restore VP
    public bool gotRestoreVPPower;
    public float restoreVP; // Restore VP

    public float timeBetweenConsume = 1f;


    private void Awake()
    {
        instance = this;
        currentVP = maxVP;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHP = player.GetComponent<PlayerHP>();
    }


    // Update is called once per frame
    void Update()
    {
        // Consume VP 
        timeBetweenConsume -= Time.deltaTime;
        if (timeBetweenConsume <= 0)  // Can only have superpowers during superpower time
        {
            ConsumeVP();
            timeBetweenConsume = 1;
        }

        if (restoreVP != 0) // Restore VP
        {
            currentVP = currentVP + restoreVP;
            if (currentVP >= 100) currentVP = 100;
            restoreVP = 0;
        }

        // Change vpBar display
        if (currentVP <= 0)
        {
            vpBar.fillAmount = 0;
            playerHP.ConsumeHP();

        }
        else if (currentVP >= 100)
        {
            currentVP = 100;
        }
        else
        {
            vpBar.fillAmount = (float)currentVP / (float)maxVP;
        }
    }

    void ConsumeVP()
    {
        currentVP = currentVP - consumeVP;
    }


}
