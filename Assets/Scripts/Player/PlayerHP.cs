using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{

    private int PlayerMaxHP = 100;
    public Slider Slider;
    public Text winloseText;
    public int PlayerCurrentHP;
    private bool isServer = true;

    private void Start()
    {
        PlayerCurrentHP = PlayerMaxHP;
        Slider.value = PlayerMaxHP;
    }

    public void TakeDamage(int damage)
    {
        if (!isServer) return;
        PlayerCurrentHP -= damage;
        ShowHPSlider();
        if (PlayerCurrentHP <= 0)
        {
            PlayerCurrentHP = 0;
            this.gameObject.SetActive(false);
            winloseText.text = "Dead....";
            Invoke("Restart", 1.5f);
        }
    }
    public void ShowHPSlider()
    {
        Slider.value = PlayerCurrentHP / (float)PlayerMaxHP;
    }

    void Restart()
    {
        //reset game 
        SceneManager.LoadScene(0);
    }
}
