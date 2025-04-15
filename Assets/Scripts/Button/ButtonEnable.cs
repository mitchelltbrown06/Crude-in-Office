using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnable : MonoBehaviour
{
    public ButtonManager buttonManager;
    public int currentSlot;
    // Start is called before the first frame update
    void Start()
    {
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
    }

    public void Enable()
    {
        if(!buttonManager.slot1Filled)
        {
            this.enabled = true;
            transform.position = buttonManager.slot1Position;
            buttonManager.slot1Filled = true;
            currentSlot = 1;
        }
    }
}
