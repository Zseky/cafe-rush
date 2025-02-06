using UnityEngine;
using TMPro;

public class TrashScript : ObjectScript
{
    public GameObject itemHolder;

    public ItemHolderScript occupiedStatus;

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
        offUserIndicator();
        if (occupiedStatus.occupiedSlot == true)
        {
            onUserIndicator();
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
                holderItem.emptySlot();

                offUserIndicator();
            }
            
        }

    }





}
