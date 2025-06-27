using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    public GameObject tombstone;
    public void Kill()
    {
        if(GetComponent<npcStats>().laserTag == true)
        {
            GameObject.FindObjectOfType<LogicScript>().laserTagPlayers.Remove(gameObject);
        }
        
        Instantiate(tombstone, transform.position, transform.rotation);
        Destroy(transform.root.gameObject);
    }
}
