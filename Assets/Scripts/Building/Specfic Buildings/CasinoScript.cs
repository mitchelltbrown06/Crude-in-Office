using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoScript : MonoBehaviour
{
    public bool timerStarted;
    public float timerDuration;
    public float timer = 0;
    public JobScript winnerJob;
    public GameObject winner;
    public bool winnerSelected;
    private GlobalStats globalStats;

    public float jackpot;

    Coroutine setNewWinner;

    // Update is called once per frame
    void Start()
    {
        globalStats = FindObjectOfType<GlobalStats>();
        setNewWinner = StartCoroutine(SetNewWinner());
    }
    void Update()
    {
        if(winnerJob != null)
        {
            if(winnerJob.occupied == true && winnerSelected == false)
            {
                winner = winnerJob.employee;
                winnerSelected = true;
            }
        if(winner != null)
        {
            if(winner != winnerJob.employee)
            {
                globalStats.SpendMoney(-jackpot);
                winnerSelected = false;
                winnerJob.price = GetComponent<SetPrices>().jobPrice;
                winnerJob = null;
                setNewWinner = StartCoroutine(SetNewWinner());
            }
        }
        }
    }
    IEnumerator SetNewWinner()
    {
        yield return new WaitForSeconds(3f);
        if(winnerJob == null)
        {
            winnerJob = GetComponentsInChildren<JobScript>()[Random.Range(0, GetComponentsInChildren<JobScript>().Length)];
            winnerJob.price = jackpot;
        }
    }
}
