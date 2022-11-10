using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropsManager : MonoBehaviour
{

    private PlayerController playerMovement;
    private GameObject player;

    //public PlayerHealth playerHealth;
    public GameObject props;

    public Transform spawnPoint;
    public float spawnTime = 2f;
    public int propsNumber = 10; // Initial number of bonus
    public int propsGainNumber = 8; // Initial number of bonus

    private PropsBehavior bonusBehavior;
    private float spawnTimeVal = 2; // Generation interval time

    public static PropsManager instance;
    private bool spawnNew;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerController>();
        // Spawn initial bonus items
        int i = 0;
        while (i < propsNumber)
        {
            Spawn();
            i++;
        }


    }



    void Spawn()
    {
        // Spawn this prop at a random location
        Instantiate(props, (new Vector3(Random.Range(-60f, 60f), 0.5f, Random.Range(-60f, 60))), spawnPoint.rotation);

    }

    public void SpawnProps()
    {
        spawnNew = true;
        propsNumber--; // Current props number decrease 
        propsGainNumber++; // Increase in props acquired by players

    }


    private void Update()
    {
        if (spawnNew)
        {
            //Debug.Log("new bonus");
            spawnTimeVal -= Time.deltaTime;
            if (spawnTimeVal <= 0)
            {
                Spawn();
                propsNumber++;
                spawnTimeVal = 2;
                spawnNew = false;
                // Spawn a new one after 2 seconds

            }
            //spawnNew = false;

        }
        HUD.instance.SetPropsNumber(propsGainNumber);
    }

    /*    public int getNumber()
        {
            return propsNumber;
        }*/


    // Shortcut bar to use props
    public void OnBonusClicked()
    {
        if (propsGainNumber != 0)
        {
            playerMovement.gotSuperpower = true;
            propsGainNumber--;
        }
    }

}
