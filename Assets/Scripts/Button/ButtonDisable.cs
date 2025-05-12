using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{
    public ButtonManager buttonManager;
    // Start is called before the first frame update
    void Start()
    {
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
    }

    public void Disable()
    {
        ClearSlot();
        for(int i = 0; i < buttonManager.buttonsSpawned.Count; i++)
        {
            if(buttonManager.buttonsSpawned[i] != this.gameObject.GetComponent<Button>())
            {
                buttonManager.buttonsSpawned[i].GetComponent<ButtonEnable>().UpdatePosition();
            }
        }
        buttonManager.buttonsSpawned.Remove(gameObject.GetComponent<Button>());
        Destroy(gameObject);
    }
    void ClearSlot()
    {
        if (gameObject.GetComponent<ButtonEnable>().currentSlot == 1)
        {
            buttonManager.slot1Filled = false;
        }
        else if (gameObject.GetComponent<ButtonEnable>().currentSlot == 2)
        {
            buttonManager.slot2Filled = false;
        }
        else if (gameObject.GetComponent<ButtonEnable>().currentSlot == 3)
        {
            buttonManager.slot3Filled = false;
        }
    }
}
