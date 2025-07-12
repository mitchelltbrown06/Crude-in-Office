using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcJob : MonoBehaviour
{
    private npcStats myStats;
    public Node jobNode;
    public Node queueNode;
    public Node waitingRoomNode;

    public bool jobToDo = false;

    void Start()
    {
        myStats = GetComponent<npcStats>();
    }
    
    public void JobComplete(float price)
    {
        jobToDo = false;
        myStats.SpendMoney(price);
    }
}
