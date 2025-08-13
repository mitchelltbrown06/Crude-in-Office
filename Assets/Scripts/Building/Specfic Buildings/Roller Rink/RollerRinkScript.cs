using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RollerRinkScript : MonoBehaviour
{
    public GameObject boostedEmployee;
    public JobScript jobScript;
    public GridScript grid;
    public GameObject logic;
    public GameObject door;
    public float distanceToDoor;
    private float randomNumber;
    private GlobalStats stats;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic");
        stats = GameObject.FindObjectOfType<GlobalStats>();
        door = transform.root.transform.Find("Door").gameObject;
        jobScript = GetComponent<JobScript>();
        grid = FindObjectOfType<GridScript>();
        randomNumber = UnityEngine.Random.Range(-grid.tileSize * .4f, grid.tileSize * .2f);
        transform.parent.GetComponent<Animator>().speed = 1 / (Vector2.Distance(transform.position + (transform.position - transform.parent.transform.position).normalized * randomNumber, transform.parent.transform.position)
                                                        / Vector2.Distance(transform.position, transform.parent.position)) + UnityEngine.Random.Range(-.2f, .2f);
        transform.position = transform.position + (transform.position - transform.parent.transform.position).normalized * randomNumber;
        distanceToDoor = Vector2.Distance(transform.position, transform.root.transform.Find("Door").transform.position);
    }

    void Update()
    {
        if (jobScript.occupied == true
        && jobScript.employee != boostedEmployee
        && Vector2.Distance(transform.position, jobScript.employee.transform.position) < distanceToDoor)
        {
            boostedEmployee = jobScript.employee;
            boostedEmployee.GetComponent<npcStats>().speed = boostedEmployee.GetComponent<npcStats>().speed * stats.rollerRinkSpeedModifier;
            transform.parent.GetComponent<Animator>().speed = transform.parent.GetComponent<Animator>().speed * logic.GetComponent<GlobalStats>().npcBaseSpeed / boostedEmployee.GetComponent<npcStats>().speed * .6f;
            transform.parent.GetComponent<Animator>().SetTrigger("StartAnimation");
            jobScript.employee.GetComponent<npcStats>().rollerSkates = true;
        }
        if (jobScript.occupied == false)
        {
            transform.parent.GetComponent<Animator>().SetTrigger("StopAnimation");
            transform.parent.transform.rotation = transform.root.transform.rotation;
        }
    }
}
