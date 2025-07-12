using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerRinkSpawner : MonoBehaviour
{
    public int maxSkaters;
    public GameObject skaterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxSkaters; i++)
        {
            GameObject newSkater = Instantiate(skaterPrefab, transform.position, transform.rotation, transform);
            newSkater.GetComponentInChildren<Node>().connections.Add(transform.Find("Door").GetComponent<Node>());
            transform.Find("Door").GetComponent<Node>().connections.Add(newSkater.GetComponentInChildren<Node>());

        }
    }
}
