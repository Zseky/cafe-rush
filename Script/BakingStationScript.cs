using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class BakingStationScript:ObjectScript
{
    [SerializeField] GameObject RecipePickUI;
    [SerializeField] TextMeshPro MainText;

    public List<string> ingredientsRequired;
    public Recipe chosenRecipe;

    enum State { Bake, Process, Complete }
    private State currentState = State.Bake;


    private ItemHolderScript itemHolder;

    private void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder").GetComponent<ItemHolderScript>();
      
    }
    public override void UseButtonFunction()
    {
        switch (currentState)
        {
            case State.Bake:
                bakeTextActivate();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    RecipePickUI.gameObject.SetActive(true);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove=false;
                    currentState = State.Process;
                }
                break;
            case State.Process:
                if (RecipePickUI.activeSelf) return;
                putTextActivate();
                offUserIndicator();
                if (!ingredientsRequired.Contains(itemHolder.occupyName)) return;
                    onUserIndicator();
                if (Input.GetKeyDown(KeyCode.F))
                {    
                    ingredientsRequired.Remove(itemHolder.occupyName);
                    itemHolder.emptySlot();  
                    if (ingredientsRequired.Count < 1)
                    {
                        currentState = State.Complete;
                    }
                   
                }
                break;
            case State.Complete:
                if (itemHolder.occupiedSlot)
                {
                    offUserIndicator();
                    return; 
                }
                    
                getTextActivate();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
                    holderItem.occupySlot(chosenRecipe.itemName, chosenRecipe.itemIcon);

                    chosenRecipe = null;
                    ingredientsRequired.Clear();

                    currentState = State.Bake;
                }
                break;
        }

    }

    void putTextActivate()
    {
        MainText.text = "Put";
    }

    void bakeTextActivate()
    {
        MainText.text = "Bake";
    }

    void getTextActivate()
    {
        MainText.text = "Get";
    }
}
