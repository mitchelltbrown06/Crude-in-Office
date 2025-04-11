using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShot : MonoBehaviour
{
    public float health = 10;
    public LogicScript logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    public void TakeDamage()
    {
        Debug.Log("Got Shot");
        health -= logic.vanceShotDamage;
    }
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
