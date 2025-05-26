using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcStats : MonoBehaviour
{
    public GlobalStats globalStats;

    public float money;
    public float speed;

    void Start()
    {
        speed = .3f;
        globalStats = GameObject.FindObjectOfType<GlobalStats>();
    }

    public void SpendMoney(float moneySpent)
    {
        money -= moneySpent;
        globalStats.MakeMoney(moneySpent);
    }
    public void MakeMoney(float moneyMade)
    {
        money += moneyMade;
    }
}
