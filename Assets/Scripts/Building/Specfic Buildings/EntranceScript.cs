using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    public GameObject exit;
    public GameObject npc;
    public float spawnCooldown;
    public float Timer;
    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        Timer = spawnCooldown;
        logic.FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
    }
    void Update()
    {
        if (exit != null)
        {
            Timer += Time.deltaTime;
            if(Timer > spawnCooldown && exit != null)
            {
                if (AStarManager.instance.GeneratePath(
                logic.FindNearestNode(transform.position),
                logic.FindNearestNode(exit.transform.position)) != null)
                {
                    Timer = 0;
                    logic.npcs.Add(Instantiate(npc, transform.position, Quaternion.identity));
                }
            }
        }
    }
}