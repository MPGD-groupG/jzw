using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{

    private int maxHP = 100;
    public int currentHP;
    public Slider Slider;
    public Text winloseText;
    // public int PlayerCurrentHP;
    private bool isServer = true;

    public static PlayerHP instance;

    private PlayerController playerController;
    private bool isDead;

    // Restore HP
    public bool gotRestoreHPPower;
    public int restoreHP = 20; // Restore 20 HP/s
    private float time;
    public float superTimeVal = 10; // Superpower Duration


    public Image hpBar;


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
            gotRestoreHPPower=false;
        }


        // Change hpBar display
        if (currentHP <= 0)
        {
            hpBar.fillAmount = 0;
            UIManager.instance.checkState();  // Gameover
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
            UIManager.instance.checkState();
            /*            this.gameObject.SetActive(false);
                        winloseText.text = "Dead....";
                        Invoke("Restart", 1.5f);*/

        }
    }


    public void ShowHPSlider()
    {
        Slider.value = currentHP / (float)maxHP;
    }


    public void RestoreHP()
    {
        Debug.Log("restore!!");
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
