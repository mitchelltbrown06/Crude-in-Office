using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcStats : MonoBehaviour
{
    public GlobalStats globalStats;

    public float money;
    public float speed;
    public float hunger;
    public float bathroom;

    public bool rollerSkates;
    public bool laserTag;

    public float hungerMultiplier;
    public float bathroomMultiplier;

    public bool adult;

    public LogicScript logic;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        globalStats = GameObject.FindObjectOfType<GlobalStats>();
        adult = logic.WeightedCoinToss(.666f);
    }
    void Update()
    {
        hunger += Time.deltaTime * hungerMultiplier;
        bathroom += Time.deltaTime * bathroomMultiplier;
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
