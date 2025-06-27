using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoScript : MonoBehaviour
{
    public bool timerStarted;
    public float timerDuration;
    public float timer = 0;

    public float price;

    public JobScript winnerJob;
    public GameObject winner;
    public bool winnerSelected;

    public float jackpot;

    Coroutine setNewWinner;

    // Update is called once per frame
    void Start()
    {
        foreach(JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNode.price = price;
        }
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
                FindObjectOfType<GlobalStats>().SpendMoney(-jackpot);
                Debug.Log("spent money");
                winnerSelected = false;
                winnerJob.price = price;
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
