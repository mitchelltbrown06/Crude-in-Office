using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRigScript : MonoBehaviour
{
    public LogicScript Logic;
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if( health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        Logic.DigWithOilRig();
    }
    public void DamageFromJoe()
    {
        health -= 10;
    }
}
