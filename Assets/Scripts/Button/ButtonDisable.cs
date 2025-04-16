using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(gameObject);
    }
    void ClearSlot()
    {
        if (gameObject.GetComponent<ButtonEnable>().currentSlot == 1)
        {
            buttonManager.slot1Filled = false;
        }
        if (gameObject.GetComponent<ButtonEnable>().currentSlot == 2)
        {
            buttonManager.slot2Filled = false;
        }
    }
}
