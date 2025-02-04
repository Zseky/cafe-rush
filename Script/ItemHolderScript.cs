using UnityEngine;

public class ItemHolderScript : MonoBehaviour, IHeldItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool occupiedSlot = false;
    public string occupyName;
    public Sprite occupySprite;



    public void occupySlot(string itemName, Sprite itemSprite)

    {
        occupiedSlot = true;
        occupyName = itemName;
        occupySprite = itemSprite;

        GetComponent<SpriteRenderer>().sprite = occupySprite;
    }

    public void emptySlot()
    {
        occupiedSlot = false;
        occupyName = null;
        occupySprite = null;

        GetComponent<SpriteRenderer>().sprite = null;
    }


}
