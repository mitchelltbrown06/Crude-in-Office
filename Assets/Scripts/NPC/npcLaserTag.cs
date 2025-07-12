using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcLaserTag : MonoBehaviour
{
    public float attackTimer;
    public float attackCooldown;
    public float attackRange;
    public float shotCost;

    public LineRenderer lineRenderer;

    public LogicScript logic;
    private GlobalStats globalStats;

    Coroutine stopLaser;

    void Start()
    {
        globalStats = GameObject.FindObjectOfType<GlobalStats>();
        logic = GameObject.FindObjectOfType<LogicScript>();
        lineRenderer = GetComponent<LineRenderer>();
        attackTimer = Random.Range(0, attackCooldown);
    }

    void Update()
    {
        if(GetComponent<npcStats>().laserTag == true)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackCooldown)
            {
                FindOpponent();
            }
        }
    }
    public void FindOpponent()
    {
        foreach(GameObject opponent in logic.laserTagPlayers)
        {
            if(opponent != null && Vector2.Distance(transform.position, opponent.transform.position) < attackRange)
            {
                Shoot(opponent);
                return;
            }
        }
    }
    public void GetHit()
    {

    }
    public void Shoot(GameObject opponent)
    {
        lineRenderer.enabled = true;
        opponent.GetComponent<npcLaserTag>().GetHit();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, opponent.transform.position);
        attackTimer = Random.Range(0, .5f);
        stopLaser = StartCoroutine(StopLaser());
        globalStats.MakeMoney(globalStats.laserTagCost);
    }

    IEnumerator StopLaser()
    {
        yield return new WaitForSeconds(.1f);
        lineRenderer.enabled = false;
    }
}
