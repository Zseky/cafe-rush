using UnityEngine;
using TMPro;

public class EmptyTableScript : ObjectScript
{
    [SerializeField] GameObject itemHolder;

    public ItemHolderScript occupiedStatus;

    public TextMeshPro indicatorText;

    
    string tableItemName;
    Sprite tableItemSprite;

    [SerializeField] SpriteRenderer tableHolderSprite;



    void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
        occupiedStatus = itemHolder.GetComponent<ItemHolderScript>(); 
    }

    public override void onUserIndicator()
    {
        base.onUserIndicator();
    }



    public override void UseButtonFunction()
    {
        if (occupiedStatus.occupiedSlot == true)
        {
            
            if (tableItemName != null)
            {
                swapTextActivate();
                if (!Input.GetKeyDown(KeyCode.F)) return;


                IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();

                string swapItemName = tableItemName;
                Sprite swapItemSprite = tableItemSprite;

                tableItemName = occupiedStatus.occupyName;
                tableItemSprite = occupiedStatus.occupySprite;

                holderItem.occupySlot(swapItemName, swapItemSprite);

            }
            else
            {
                putTextActivate();

                if (!Input.GetKeyDown(KeyCode.F)) return;


                IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();

                tableItemName = occupiedStatus.occupyName;
                tableItemSprite = occupiedStatus.occupySprite;

                holderItem.emptySlot();

            }
        }
        else
        {
            if (tableItemName != null)
            {

                getTextActivate();
                if (!Input.GetKeyDown(KeyCode.F)) return;
                IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
                holderItem.occupySlot(tableItemName, tableItemSprite);

                tableItemName = null;
                tableItemSprite = null;
            }

            offUserIndicator();
        }

        tableHolderSprite.sprite = tableItemSprite;


    }

    void swapTextActivate()
    {
        indicatorText.text = "Swap";
    }

    void putTextActivate()
    {
        indicatorText.text = "Put";
    }

    void getTextActivate()
    {
        indicatorText.text = "Get";
    }



}
