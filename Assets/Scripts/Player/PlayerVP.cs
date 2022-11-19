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


    public int maxVP = 100; // Initial VP
    public int currentVP;

    public int consumeVP = 5; // Cost 5 SP/s
    public int restoreVP = 20; // Restore 20 SP/s

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

        // Change vpBar display
        if (currentVP <= 0)
        {
            vpBar.fillAmount = 0;
            playerHP.ConsumeHP();

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
