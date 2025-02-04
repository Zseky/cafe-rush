using UnityEngine;
using TMPro;

public class TrashScript : ObjectScript
{
    public GameObject itemHolder;

    public ItemHolderScript occupiedStatus;


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

        if (occupiedStatus.occupiedSlot == true)
        {
            showIndicator = true;

            if (!Input.GetKeyDown(KeyCode.F)) return;


            IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
            holderItem.emptySlot();

            showIndicator = false;
            
        }
        else
        {
            showIndicator = false;
        }

    }





}
