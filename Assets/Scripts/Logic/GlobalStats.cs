using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    private PerkEvent perkEvent;
    public float money;
    public float laserTagCost;
    public float salesTax = 0;
    public int perkOptions;
    public float rollerRinkSpeedModifier;
    public float rollerRinkBaseSpeed;
    public float npcBaseSpeed;
    public float npcSpeed;
     public float hungryCutoff;
    public float starvation;
    public float bathroomCutoff;
    public float gottaGoCuttoff;
    public int richCutoff;
    void Start()
    {
        npcSpeed = npcBaseSpeed;
        rollerRinkSpeedModifier = rollerRinkBaseSpeed;
        perkEvent = FindObjectOfType<PerkEvent>();
    }
    public void SpendMoney(float moneySpent)
    {
        money -= moneySpent;
    }
    public void MakeMoney(float moneyMade)
    {
        if (moneyMade > 0)
        {
            money += moneyMade + salesTax;   
            Debug.Log("current money :" + money.ToString());
        }

        //if you now have more money than the next perk even threshold, trigger a perk event
        if (money >= perkEvent.threshold && !perkEvent.perkEventActive)
        {
            perkEvent.TriggerPerkEvent();
        }
    }
}
