using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    public GameObject exit;
    public GameObject npc;

    public bool exitFound = false;

    public float spawnCooldown;
    public float Timer;

    void Start()
    {

    }
    void Update()
    {
        if(FindObjectOfType<ExitScript>() && exitFound == false)
        {
            exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
            exitFound = true;
        }
        Timer += Time.deltaTime;
        if(Timer > spawnCooldown && exit != null && AStarManager.instance.GeneratePath(AStarManager.instance.FindNearestNode(transform.position), AStarManager.instance.FindNearestNode(exit.transform.position)) != null)
        {
            Timer = 0;
            Instantiate(npc, transform.position, Quaternion.identity);
        }
    }
}