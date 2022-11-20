using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

    private PlayerController playerMovement;
    private GameObject player;
    public GameObject props;
    private float curtTime = 0.0f;

    public float timeBetweenConsume = 1f;
    public int playerTouchedOnce = 0; // Number of times touched item by player, avoid multiple trigger

    public static ItemBehavior instance;
    private int itemScore = 10; // item score

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerController>();

    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Only for the 1st touched
            playerTouchedOnce++;
            if (playerTouchedOnce == 1)
            {
                UIManager.instance.SetScoreText();
                ItemManager.instance.SpawnProps(); // Spawn new item
                // Debug.Log("destroy pickup");
            }

        }

    }


}
