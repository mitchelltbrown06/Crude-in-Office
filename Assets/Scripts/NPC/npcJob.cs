using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcJob : MonoBehaviour
{
    private npcController myController;
    private npcStats myStats;

    public bool jobToDo = false;

    void Start()
    {
        myStats = GetComponent<npcStats>();
        myController = GetComponent<npcController>();
    }
    
    public void JobComplete(float price)
    {
        jobToDo = false;
        myStats.SpendMoney(price);
    }
}
