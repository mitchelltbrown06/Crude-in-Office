using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkEvent : MonoBehaviour
{
    public float threshold;
    public float startingThreshold;
    public float thresholdMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        threshold = startingThreshold;
    }

    public void TriggerPerkEvent()
    {
        //pick 3 perk options
        //present 3 button options
        //update threshold
        threshold = threshold * thresholdMultiplier;
        //Debug.Log("You just got a perk!");
    }
}
