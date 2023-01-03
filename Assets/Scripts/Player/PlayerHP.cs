using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerHP : MonoBehaviour
{
    private PlayerController playerController;
    public static PlayerHP instance;
    public Slider Slider;
    public Text winloseText;
    public Image hpBar;

    private float maxHP = 100;
    public float currentHP;

    // public int PlayerCurrentHP;
    private bool isServer = true;
    private bool isDead;
    public bool isGod;

    // Restore HP
    public bool gotRestoreHPPower;
    public float restoreHP; // Restore x HP


    // Consume HP
    public bool isConsumeHPByVP;
    public float consumeHPByVP = 5; // Consume 5 HP/s

    private float time;
    public float timeBetweenGod = 15f; // God mode Duration
    public float timeBetweenConsumeHPByVP = 1f; // Deduct HP Duration

    private void Awake()
    {
        instance = this;
        currentHP = maxHP;

    }


    private void Start()
    {
        // currentHealth = maxHP;
        Slider.value = maxHP;
    }

    private void Update()
    {
        if (restoreHP!=0)
        {
            currentHP += restoreHP;
            restoreHP = 0;
        }

        if (isGod)
        {
            TakeDamage();
        }

        // Change hpBar display
        if (currentHP <= 0)
        {
            hpBar.fillAmount = 0;
            // HUD.instance.checkState();  // Gameover
            HUD.instance.isDead = true;
        }
        else
        {
            hpBar.fillAmount = (float)currentHP / (float)maxHP;
        }
    }


    public void TakeDamage(int damage)
    {
        if (!isServer) return;
        currentHP = currentHP - damage;
        // PlayerCurrentHP -= damage;
        ShowHPSlider();
        if (currentHP <= 0)
        {
            currentHP = 0;
            // HUD.instance.checkState();
            HUD.instance.isDead = true;
            /*            this.gameObject.SetActive(false);
                        winloseText.text = "Dead....";
                        Invoke("Restart", 1.5f);*/

        }
    }

    public void TakeDamage()
    {
        isServer = false;
        timeBetweenGod -= Time.deltaTime;
        if (timeBetweenGod <= 0)  // Can only have god mode during limited time
        {
            timeBetweenGod = 15;
            isServer = true;
            isGod = false;
        }
    }


    public void ShowHPSlider()
    {
        Slider.value = currentHP / (float)maxHP;
    }


    public void ConsumeHP()
    {

        timeBetweenConsumeHPByVP -= Time.deltaTime;
        if (timeBetweenConsumeHPByVP <= 0)  // Can only have superpowers during superpower time
        {
            //isConsumeHPByVP = false;
            currentHP = currentHP - consumeHPByVP;
            timeBetweenConsumeHPByVP = 1;
        }

        time = time + Time.deltaTime;

    }


    public void RestoreHP()
    {
        // Debug.Log("restore hp");
        if (currentHP < 100)
        {
            currentHP = currentHP + restoreHP;   // SP restore
        }
        else
        {
            currentHP = 100;
        }

    }


    void Restart()
    {
        //reset game 
        SceneManager.LoadScene(0);
    }
}
