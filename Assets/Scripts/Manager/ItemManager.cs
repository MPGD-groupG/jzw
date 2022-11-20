using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;
    public GameObject[] itemGroup; // item group

    public float spawnTime = 2f;
    public int itemNumber = 10; // Initial number of bonus
    public int itemGainNumber = 8; // Initial number of bonus

    private ItemBehavior bonusBehavior;
    private float spawnTimeVal = 2; // Generation interval time

    public static ItemManager instance;
    private bool spawnNew;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        // Spawn initial items
        int i = 0;
        while (i < itemNumber)
        {
            Spawn();
            i++;
        }

    }


    /*    // Old spawn for only one item
        void Spawn()
        {
            // Spawn this prop at a random location
            Instantiate(props, (new Vector3(Random.Range(-60f, 60f), 0.5f, Random.Range(-60f, 60))), spawnPoint.rotation);

        }*/

    public void Spawn()
    {
        // Random Index
        int i = Random.Range(0, itemGroup.Length);

        Instantiate(itemGroup[i],(new Vector3(Random.Range(-60f, 60f), 0.5f, Random.Range(-60f, 60))), Quaternion.identity);
    }


    public void SpawnProps()
    {
        spawnNew = true;
        //itemNumber--; // Current item number decrease 
        //itemGainNumber++;

    }


    private void Update()
    {
        if (spawnNew)
        {
            //Debug.Log("new item");
            spawnTimeVal -= Time.deltaTime;
            if (spawnTimeVal <= 0)
            {
                Spawn();
                itemNumber++;
                spawnTimeVal = 2;
                spawnNew = false;
                // Spawn a random new item after 2 seconds

            }
            //spawnNew = false;

        }
        HUD.instance.SetItemNumber(itemGainNumber);
    }


/*    // Shortcut bar to use props
    public void OnBonusClicked()
    {
        if (itemGainNumber != 0)
        {
            playerController.gotSpeedUpPower = true;
            itemGainNumber--;
        }
    }*/





}
