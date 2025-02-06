using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OvenScript : ObjectScript
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private GameObject itemHolder;

    private Recipe cookedRecipe;
    private float cookTime = 5f;         // Total cook time in seconds
    private float currentCookTime = 0f;  // Timer to track cooking progress

    enum State { Start, Cook, Complete };
    private State currentState = State.Start;

    void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
    }

    void Update()
    {
        // Check if the oven is currently cooking
        if (currentState == State.Cook)
        {
            currentCookTime -= Time.deltaTime;  // Decrease the timer each frame
            Debug.Log("Cooking... Time left: " + currentCookTime.ToString("F2"));

            if (currentCookTime <= 0)
            {
                currentState = State.Complete;  // Transition to Complete when time is up
                Debug.Log("Cooking Complete!");
            }
        }
    }

    public override void UseButtonFunction()
    {
        if (!itemHolder.GetComponent<ItemHolderScript>().occupiedSlot ||
            itemHolder.GetComponent<ItemHolderScript>().occupyType != "recipe")
        {
            offUserIndicator();
            return;
        }

        onUserIndicator();

        switch (currentState)
        {
            case State.Start:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
                    foreach (Recipe recipe in recipes)
                    {
                        if (recipe.itemName == itemHolder.GetComponent<ItemHolderScript>().occupyName)
                        {
                            cookedRecipe = recipe;
                            holderItem.emptySlot();
                            StartCooking();  // Start cooking when recipe is found
                            break;
                        }
                    }
                }
                break;

            case State.Complete:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
                    holderItem.occupySlot("recipe", cookedRecipe.itemName, cookedRecipe.cookedIcon);
                    currentState = State.Start;  // Reset state after completing
                    Debug.Log("Recipe collected and oven reset.");
                }
                break;
        }
    }

    private void StartCooking()
    {
        currentCookTime = cookTime;   // Initialize the timer
        currentState = State.Cook;    // Change state to Cook, this starts the timer
        Debug.Log("Started Cooking: " + cookedRecipe.itemName);
    }
}