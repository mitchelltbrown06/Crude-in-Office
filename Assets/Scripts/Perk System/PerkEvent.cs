using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkEvent : MonoBehaviour
{
    public float threshold;
    public float startingThreshold;
    public float thresholdMultiplier;
    public float numberOfOptions;
    public ButtonManager buttonManager;
    public List<Button> activeButtons;
    public List<Button> options;
    public GlobalStats stats;
    public bool perkEventActive;
    // Start is called before the first frame update
    void Start()
    {
        threshold = startingThreshold;
        TriggerPerkEvent();
    }
    public void TriggerPerkEvent()
    {
        perkEventActive = true;
        List<Button> oldOptions = new List<Button>(options);
        //pick 3 perk options
        for (int i = 0; i < numberOfOptions; i++)
        {
            Button currentOption = options[Random.Range(0, options.Count)];
            options.Remove(currentOption);
            //activeButtons.Add(buttonManager.Spawn(currentOption, new Vector3(buttonManager.canvas.transform.position.x + i * 105 * (int)Math.Pow(-1, i), buttonManager.canvas.transform.position.y, 0)));
            activeButtons.Add(buttonManager.Spawn(currentOption, buttonManager.transform.position));
            if (i > 0)
            {
                transform.position = new Vector2(transform.position.x - 50, transform.position.y);

            }
        }
        //present 3 button options
        //update threshold
        threshold = threshold * thresholdMultiplier;
        //Debug.Log("You just got a perk!");

        options = oldOptions;
    }
    public void EndPerkEvent()
    {
        transform.position = transform.root.transform.position;
        perkEventActive = false;
        foreach (Button button in activeButtons)
        {
            Destroy(button.gameObject);
        }
        activeButtons.Clear();
    }
}
