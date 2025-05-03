using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    public float money;

    public void SpendMoney(float moneySpent)
    {
        money -= moneySpent;
    }
    public void MakeMoney(float moneyMade)
    {
        money += moneyMade;
        Debug.Log("current money :" + money.ToString());
    }
}
