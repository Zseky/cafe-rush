using UnityEngine;
using TMPro;

public class EmptyTableScript : ObjectScript
{
    [SerializeField] GameObject itemHolder;

    public ItemHolderScript occupiedStatus;

    public TextMeshPro indicatorText;

    bool tableOccupiedSlot = false;
    
    string tableItemName;
    Sprite tableItemSprite;

    [SerializeField] SpriteRenderer tableHolderSprite;


    bool showIndicator = false;


    void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
        occupiedStatus = itemHolder.GetComponent<ItemHolderScript>(); 
    }

    public override void onUserIndicator()
    {
        if (showIndicator) base.onUserIndicator();
    }



    public override void UseButtonFunction()
    {
        showIndicator = true;
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

               // showIndicator = false;


            }
            else
            {
                //showIndicator = true;
                putTextActivate();

                if (!Input.GetKeyDown(KeyCode.F)) return;


                IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();

                tableItemName = occupiedStatus.occupyName;
                tableItemSprite = occupiedStatus.occupySprite;

                holderItem.emptySlot();



                //showIndicator = false;

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
                //showIndicator = false;
            }
            

        }

        tableHolderSprite.sprite = tableItemSprite;
        showIndicator = false;


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
