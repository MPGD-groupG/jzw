using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI_Light : MonoBehaviour
{
    public Text winloseText;
    public Transform player;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SetActive(false);
            Invoke("Restart", 1f);
            //SceneManager.LoadScene(0);
        }
    }

    void Restart()
    {
        //reset game 
        SceneManager.LoadScene(0);
    }
}