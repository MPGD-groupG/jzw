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

    private int maxHP = 100;
    public int currentHP;

    // public int PlayerCurrentHP;
    private bool isServer = true;
    private bool isDead;
    public bool isGod;

    // Restore HP
    public bool gotRestoreHPPower;
    public int restoreHP = 20; // Restore 20 HP/s

    // Consume HP
    public bool consumeHPByVP;
    public int consumeHP = 10; // Consume 10 HP/s

    private float time;
    public float timeBetweenGod = 5f; // God mode Duration
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
        if (gotRestoreHPPower)
        {
            currentHP += 5;
            gotRestoreHPPower = false;
        }

        if (isGod)
        {
            TakeDamage();
        }

        // Change hpBar display
        if (currentHP <= 0)
        {
            hpBar.fillAmount = 0;
            // UIManager.instance.checkState();  // Gameover
            UIManager.instance.isDead = true;
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
            // UIManager.instance.checkState();
            UIManager.instance.isDead = true;
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
            timeBetweenGod = 5;
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
            //consumeHPByVP = false;
            currentHP = currentHP - consumeHP;
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
