using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicks : MonoBehaviour
{
    public void SalesTax()
    {
        GameObject logic = GameObject.FindGameObjectWithTag("Logic");
        logic.GetComponent<GlobalStats>().salesTax += 1;
        GameObject.FindObjectOfType<PerkEvent>().EndPerkEvent();
    }
    public void StickyWheels()
    {
        GameObject logic = GameObject.FindGameObjectWithTag("Logic");
        logic.GetComponent<GlobalStats>().rollerRinkSpeedModifier = logic.GetComponent<GlobalStats>().rollerRinkSpeedModifier * .6f;
        GameObject.FindObjectOfType<PerkEvent>().EndPerkEvent();
    }
    public void StickyFloors()
    {
        GameObject logic = GameObject.FindGameObjectWithTag("Logic");
        logic.GetComponent<GlobalStats>().npcSpeed = logic.GetComponent<GlobalStats>().npcSpeed * .8f;
        GameObject.FindObjectOfType<PerkEvent>().EndPerkEvent();
    }
    public void BigBladder()
    {
        GameObject logic = GameObject.FindGameObjectWithTag("Logic");
        logic.GetComponent<GlobalStats>().gottaGoCuttoff = logic.GetComponent<GlobalStats>().gottaGoCuttoff * 1.5f;
        GameObject.FindObjectOfType<PerkEvent>().EndPerkEvent();
    }
    public void LaserTax()
    {
        GameObject logic = GameObject.FindGameObjectWithTag("Logic");
        logic.GetComponent<GlobalStats>().laserTagCost++;
        GameObject.FindObjectOfType<PerkEvent>().EndPerkEvent();
    }
}
