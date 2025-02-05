using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientScript : ObjectScript
{
    

    [SerializeField] private Ingredients ingredientType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject itemHolder; 
    void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
    }



    public override void UseButtonFunction()
    {
        if (itemHolder.GetComponent<ItemHolderScript>().occupiedSlot) 
        {
            offUserIndicator();
            return; 
        }
        onUserIndicator();
                
        if (Input.GetKeyDown(KeyCode.F))
        {
            IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
            holderItem.occupySlot(ingredientType.itemName, ingredientType.itemSprite);
        }
        
       
    }

    
    
}
