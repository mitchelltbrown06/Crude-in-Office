using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnable : MonoBehaviour
{
    public ButtonManager buttonManager;
    public int currentSlot;
    // Start is called before the first frame update

    public void Enable()
    {
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        buttonManager.buttonsSpawned.Add(gameObject.GetComponent<Button>());
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
        else if(!buttonManager.slot3Filled)
        {
            this.enabled = true;
            transform.position = buttonManager.slot3Position;
            buttonManager.slot3Filled = true;
            currentSlot = 3;
        }
        else if(!buttonManager.slot4Filled)
        {
            this.enabled = true;
            transform.position = buttonManager.slot4Position;
            buttonManager.slot4Filled = true;
            currentSlot = 4;
        }
        else if(!buttonManager.slot5Filled)
        {
            this.enabled = true;
            transform.position = buttonManager.slot5Position;
            buttonManager.slot5Filled = true;
            currentSlot = 5;
        }
    }
    public void UpdatePosition()
    {
        if(buttonManager != null)
        {
            if (!buttonManager.slot1Filled && currentSlot == 2)
            {
                transform.position = buttonManager.slot1Position;
                buttonManager.slot2Filled = false;
                buttonManager.slot1Filled = true;
                currentSlot = 1;
            }
            if(!buttonManager.slot2Filled && currentSlot == 3)
            {
                transform.position = buttonManager.slot2Position;
                buttonManager.slot3Filled = false;
                buttonManager.slot2Filled = true;
                currentSlot = 2;
            }
            if(!buttonManager.slot3Filled && currentSlot == 4)
            {
                transform.position = buttonManager.slot3Position;
                buttonManager.slot4Filled = false;
                buttonManager.slot3Filled = true;
                currentSlot = 3;
            }
            if(!buttonManager.slot4Filled && currentSlot == 5)
            {
                transform.position = buttonManager.slot4Position;
                buttonManager.slot5Filled = false;
                buttonManager.slot4Filled = true;
                currentSlot = 4;
            }
        }
        buttonManager.CheckSpawnPosition();
    }
}
