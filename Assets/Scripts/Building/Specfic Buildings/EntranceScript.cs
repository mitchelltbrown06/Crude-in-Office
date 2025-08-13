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
    public GameObject spawnPoint;

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
            if (Timer > spawnCooldown && exit != null)
            {
                Timer = 0;
                logic.npcs.Add(Instantiate(npc, spawnPoint.transform.position, Quaternion.identity));
            }
        }
    }
}