using UnityEngine;

public class ItemHolderScript : MonoBehaviour, IHeldItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool occupiedSlot = false;
    public string occupyType;
    public string occupyName;
    public Sprite occupySprite;



    public void occupySlot(string itemType, string itemName, Sprite itemSprite)

    {
        occupyType = itemType;
        occupiedSlot = true;
        occupyName = itemName;
        occupySprite = itemSprite;

        GetComponent<SpriteRenderer>().sprite = occupySprite;
    }

    public void emptySlot()
    {
        occupyType = null;
        occupiedSlot = false;
        occupyName = null;
        occupySprite = null;

        GetComponent<SpriteRenderer>().sprite = null;
    }


}
