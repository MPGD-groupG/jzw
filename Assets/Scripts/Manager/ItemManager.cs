using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;
    public GameObject[] itemGroupSafe; // item group
    public GameObject[] itemGroupApple; // item group
    public GameObject[] itemGroupHero; // item around hero
    public GameObject[] itemGroupSnow; // item on the snow field


    public float spawnTime = 2f;
    public int itemNumberSafe = 10; // Initial number of item
    public int itemNumberApple = 30; // Initial number of items
    public int itemNumberHero = 5; // Initial number of items
    public int itemNumberSnow = 15; // Initial number of items


    private ItemBehavior bonusBehavior;
    private float spawnTimeVal = 2; // Generation interval time
    private float spawnTimeValSafe = 20;

    public static ItemManager instance;
    private bool spawnNew;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        // Spawn initial items
        int i = 0;
        while (i < itemNumberSafe)
        {
            SpawnSafe();
            i++;
        }
        int a = 0;
        while (a < itemNumberApple)
        {
            SpawnApple();
            a++;
        }
/*        int b = 0;
        while (b < itemNumberHero)
        {
            SpawnHero();
            b++;
        }*/
        int c = 0;
        while (c < itemNumberSnow)
        {
            SpawnSnow();
            c++;
        }

    }

    private void SpawnSnow()
    {
        int i = Random.Range(0, itemGroupSnow.Length);
        Instantiate(itemGroupSnow[i], (new Vector3(Random.Range(70f, 260f), 30f, Random.Range(20f, 265f))), Quaternion.identity);
    }

    private void SpawnHero()
    {
        int i = Random.Range(0, itemGroupHero.Length);
        Instantiate(itemGroupHero[i], (new Vector3(Random.Range(player.transform.position.x - 50f, player.transform.position.x + 50f), player.transform.position.y, Random.Range(player.transform.position.z - 50f, player.transform.position.z + 50f))), Quaternion.identity);
    }

    private void SpawnApple()
    {
        int i = Random.Range(0, itemGroupApple.Length);
        Instantiate(itemGroupApple[i], (new Vector3(Random.Range(276f, 130f), 31.5f, Random.Range(-140f, -0f))), Quaternion.identity);
    }

    public void SpawnSafe()
    {
        // Random Index
        int i = Random.Range(0, itemGroupSafe.Length);

        Instantiate(itemGroupSafe[i],(new Vector3(Random.Range(120f, 135f), 32f, Random.Range(-95f, -90f))), Quaternion.identity);
    }


    private void Spawn()
    {
        int i = Random.Range(0, itemGroupSafe.Length);

        Instantiate(itemGroupSafe[i], (new Vector3(Random.Range(119f, 135f), 31.5f, Random.Range(-80f, -70f))), Quaternion.identity);
    }

    public void SpawnProps()
    {
        spawnNew = true;
        //itemNumberSafe--; // Current item number decrease 
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
                SpawnHero();
                //itemNumberSafe++;
                spawnTimeVal = 2;
                spawnNew = false;
                // Spawn a random new item after 2 seconds

            }
            //spawnNew = false;

        }

        if (itemNumberSafe <= 10)
        {
            //Debug.Log("new item");
            spawnTimeValSafe -= Time.deltaTime;
            if (spawnTimeValSafe <= 0)
            {
                SpawnSafe();
                itemNumberSafe++;
                spawnTimeValSafe = 20;
                spawnNew = false;
                // Spawn a random new item after 20 seconds

            }
            //spawnNew = false;

        }

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
