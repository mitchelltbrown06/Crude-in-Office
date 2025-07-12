using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    public float money;
    public float laserTagCost;
    private PerkEvent perkEvent;
    void Start()
    {
        perkEvent = FindObjectOfType<PerkEvent>();
    }
    public void SpendMoney(float moneySpent)
    {
        money -= moneySpent;
    }
    public void MakeMoney(float moneyMade)
    {
        money += moneyMade;
        if (moneyMade > 0)
        {
            //Debug.Log("current money :" + money.ToString());
        }

        //if you now have more money than the next perk even threshold, trigger a perk event
        if (money >= perkEvent.threshold)
        {
            perkEvent.TriggerPerkEvent();
        }
    }
}
