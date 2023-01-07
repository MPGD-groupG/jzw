using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;
    // item group
    public GameObject[] itemGroupSafe; // item group
    public GameObject[] itemGroupApple; // item group
    public GameObject[] itemGroupHero; // item around hero
    public GameObject[] itemGroupSnow; // item on the snow field
    public GameObject itemBonus; 
    
    //item spawn point
    public GameObject[] appleSpawnPoint;
    public GameObject[] snowSpawnPoint; 
    public GameObject[] bonusSpawnPoint;


    public float spawnTime = 2f;
    public int itemNumberSafe = 10; // Initial number of item
    public int itemNumberApple = 30; 
    public int itemNumberHero = 5; 
    public int itemNumberSnow = 15;
    public int itemNumberBonus = 10;

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
        int c = 0;
        while (c < itemNumberSnow)
        {
            SpawnSnow();
            c++;
        }
        int d = 0;
        while (d < itemNumberBonus)
        {
            SpawnBonus();
            d++;
        }

    }

    private void SpawnSnow()
    {
        int i = Random.Range(0, itemGroupSnow.Length);
        int x = Random.Range(0, snowSpawnPoint.Length);
        Instantiate(itemGroupSnow[i], (new Vector3(Random.Range(snowSpawnPoint[x].transform.position.x - 50f, snowSpawnPoint[x].transform.position.x + 50f),
            snowSpawnPoint[x].transform.position.y,
            Random.Range(snowSpawnPoint[x].transform.position.z - 50f, snowSpawnPoint[x].transform.position.z + 50f))), Quaternion.identity);
    }

    private void SpawnHero()
    {
        int i = Random.Range(0, itemGroupHero.Length);
        Instantiate(itemGroupHero[i], (new Vector3(Random.Range(player.transform.position.x - 50f, player.transform.position.x + 50f),
            player.transform.position.y, 
            Random.Range(player.transform.position.z - 50f, player.transform.position.z + 50f))), Quaternion.identity);
    }

    private void SpawnApple()
    {
        int i = Random.Range(0, itemGroupApple.Length);
        int x = Random.Range(0, appleSpawnPoint.Length);
        Instantiate(itemGroupApple[i], (new Vector3(Random.Range(appleSpawnPoint[x].transform.position.x - 50f, appleSpawnPoint[x].transform.position.x + 50f),
            appleSpawnPoint[x].transform.position.y,
            Random.Range(appleSpawnPoint[x].transform.position.z - 50f, appleSpawnPoint[x].transform.position.z + 50f))), Quaternion.identity);
    }

    public void SpawnSafe()
    {
        // Random Index
        int i = Random.Range(0, itemGroupSafe.Length);

        Instantiate(itemGroupSafe[i],(new Vector3(Random.Range(120f, 135f), 32f, Random.Range(-95f, -90f))), Quaternion.identity);
    }


    public void SpawnBonus()
    {
        int x = Random.Range(0, bonusSpawnPoint.Length);
        Instantiate(itemBonus, (new Vector3(Random.Range(bonusSpawnPoint[x].transform.position.x - 20f, bonusSpawnPoint[x].transform.position.x + 20f),
            bonusSpawnPoint[x].transform.position.y,
            Random.Range(bonusSpawnPoint[x].transform.position.z - 20f, bonusSpawnPoint[x].transform.position.z + 20f))), Quaternion.identity);
    }


    private void Spawn()
    {
        int i = Random.Range(0, itemGroupSafe.Length);

        Instantiate(itemGroupSafe[i], (new Vector3(Random.Range(119f, 135f), 31.5f, Random.Range(-80f, -70f))), Quaternion.identity);
    }

    public void SpawnProps()
    {
        spawnNew = true;

    }


    private void Update()
    {
        if (spawnNew)
        {
            spawnTimeVal -= Time.deltaTime;
            if (spawnTimeVal <= 0)
            {
                // Random resources will be generated near the hero
                SpawnHero();
                spawnTimeVal = 2;
                spawnNew = false;
                // Spawn a random new item after 2 seconds

            }
        }

        if (itemNumberSafe <= 10)
        {
            spawnTimeValSafe -= Time.deltaTime;
            if (spawnTimeValSafe <= 0)
            {
                SpawnSafe();
                itemNumberSafe++;
                spawnTimeValSafe = 20;
                spawnNew = false;
                // Spawn a random new item after 20 seconds

            }
        }

    }


}
