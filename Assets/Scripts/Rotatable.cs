using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public Sprite zeroSprite;
    public Sprite ninetySprite;
    public Sprite oneEightySprite;
    public Sprite twoSeventySprite;
    void Update()
    {
        if (transform.eulerAngles == new Vector3(0, 0, 0))
        {
            GetComponent<SpriteRenderer>().sprite = zeroSprite;
        }
        if (transform.eulerAngles == new Vector3(0, 0, 90))
        {
            GetComponent<SpriteRenderer>().sprite = ninetySprite;
        }
        if (transform.eulerAngles == new Vector3(0, 0, 180))
        {
            GetComponent<SpriteRenderer>().sprite = oneEightySprite;
        }
        if (transform.eulerAngles == new Vector3(0, 0, 270))
        {
            GetComponent<SpriteRenderer>().sprite = twoSeventySprite;
        }
    }
}
