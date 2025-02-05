using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreweryScript : ObjectScript
{
    public float brewProgress = 0f;
    [SerializeField] GameObject brewIndicator;

    [SerializeField] private Coffee brewFlavor;
    enum State { Brewing, Get, Reset }
    private State currentState = State.Brewing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject itemHolder; 
    void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
    }
    void onBrewIndicator()
    {
        brewIndicator.SetActive(true);
        Slider brewValueHolder = brewIndicator.transform.Find("Slider").GetComponent<Slider>();
        brewValueHolder.value = brewProgress; 
    }

    public override void offUserIndicator()
    {
        base.offUserIndicator();
        brewIndicator?.SetActive(false);
    }
    void offBrewIndicator()
    {

        brewIndicator?.SetActive(false);
    }


    public override void UseButtonFunction()
    {
        switch (currentState)
        {
            case State.Brewing:
                if (Input.GetKey(KeyCode.F))
                {
                    if (brewProgress < 100)
                    {
                        offUserIndicator();
                        onBrewIndicator();
                        brewProgress += 1f;
                    }
                    else
                    {
                        currentState = State.Get;
                    }
                }
                else
                {
                    onUserIndicator();
                    offBrewIndicator();
                }
                break;
            case State.Get:
                GetCoffeeIndicator();
                onUserIndicator();
                
                if (Input.GetKeyDown(KeyCode.F) && itemHolder.GetComponent<ItemHolderScript>().occupiedSlot == false)
                {

                    IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
                    holderItem.occupySlot(brewFlavor.itemName, brewFlavor.itemSprite);
                    
                    
                    currentState = State.Reset;
                }
                break;
            case State.Reset:
                UseIndicatorRevert();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    brewProgress = 0;
                    UseIndicatorRevert();
                    currentState = State.Brewing;
                }
                break;
        }
       
    }

    void GetCoffeeIndicator()
    {
        UseIndicator.transform.Find("InteractIndicator").GetComponent<TextMeshPro>().text = "Get";
    }
    
    void UseIndicatorRevert()
    {
        UseIndicator.transform.Find("InteractIndicator").GetComponent<TextMeshPro>().text = "Use";
    }
    
}
