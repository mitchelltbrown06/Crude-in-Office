using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public GameObject pee;
    public Sprite happySP;
    public Sprite midSP;
    public Sprite angrySP;
    public SpriteRenderer bodySR;
    public SpriteRenderer poorSR;
    public SpriteRenderer richSR;
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        globalStats = GameObject.FindObjectOfType<GlobalStats>();
        adult = logic.WeightedCoinToss(.666f);
        bodySR.sprite = happySP;
        poorSR.enabled = false;
        richSR.enabled = false;
    }
    void Update()
    {
        hunger += Time.deltaTime * hungerMultiplier;
        bathroom += Time.deltaTime * bathroomMultiplier;
        if (bathroom > globalStats.gottaGoCuttoff)
        {
            Instantiate(pee, transform.position, transform.rotation);
            bathroom = 0;
        }
        if (money > globalStats.richCutoff)
        {
            richSR.enabled = true;
        }
        else
        {
            richSR.enabled = false;
        }
        if (money <= 0)
        {
            poorSR.enabled = true;
        }
        else
        {
            poorSR.enabled = false;
        }
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
