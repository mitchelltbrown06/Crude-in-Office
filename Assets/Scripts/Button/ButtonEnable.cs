using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnable : MonoBehaviour
{
    public ButtonManager buttonManager;
    public int currentSlot;
    // Start is called before the first frame update
    void Update()
    {
        if(buttonManager != null)
        {
            if (!buttonManager.slot1Filled && currentSlot == 2)
                {
                    transform.position = buttonManager.slot1Position;
                }
            else if(!buttonManager.slot2Filled && currentSlot == 3)
                {
                    transform.position = buttonManager.slot2Position;
                }
        }
    }

    public void Enable()
    {
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        if(!buttonManager.slot1Filled)
        {
            this.enabled = true;
            transform.position = buttonManager.slot1Position;
            buttonManager.slot1Filled = true;
            currentSlot = 1;
        }
        else if(!buttonManager.slot2Filled)
        {
            this.enabled = true;
            transform.position = buttonManager.slot2Position;
            buttonManager.slot2Filled = true;
            currentSlot = 2;
        }
    }
}
