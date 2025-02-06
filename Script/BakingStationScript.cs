using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class BakingStationScript : ObjectScript
{
    [SerializeField] private GameObject recipePickUI;
    [SerializeField] private TextMeshPro mainText;
    [SerializeField] private GameObject moldIndicator;

    private float moldProgress;
    private ItemHolderScript itemHolder;

    public List<string> ingredientsRequired = new();
    public Recipe chosenRecipe;

    private enum State { Bake, Process, Mold, Complete }
    private State currentState = State.Bake;

    private void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder").GetComponent<ItemHolderScript>();
    }

    private void UpdateMoldIndicator()
    {
        moldIndicator.SetActive(true);
        moldIndicator.transform.Find("Slider").GetComponent<Slider>().value = moldProgress;
    }

    public override void offUserIndicator()
    {
        base.offUserIndicator();
        moldIndicator?.SetActive(false);
    }

    private void HandleBakeState()
    {
        UpdateMainText("Bake");
        if (Input.GetKeyDown(KeyCode.F))
        {
            recipePickUI.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove = false;
            currentState = State.Process;
        }
    }

    private void HandleProcessState()
    {
        if (recipePickUI.activeSelf) return;
        UpdateMainText("Put");
        offUserIndicator();

        if (!ingredientsRequired.Contains(itemHolder.occupyName)) return;
        onUserIndicator();

        if (Input.GetKeyDown(KeyCode.F))
        {
            ingredientsRequired.Remove(itemHolder.occupyName);
            itemHolder.emptySlot();

            if (ingredientsRequired.Count == 0)
                currentState = State.Mold;
        }
    }

    private void HandleMoldState()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (moldProgress < 100)
            {
                offUserIndicator();
                UpdateMoldIndicator();
                moldProgress += Time.deltaTime * 100f; // Added deltaTime for frame-independent progress
            }
            else
            {
                moldProgress = 0;
                currentState = State.Complete;
                offUserIndicator();
            }
        }
        else
        {
            onUserIndicator();
            moldIndicator.SetActive(false);
        }
    }

    private void HandleCompleteState()
    {
        if (itemHolder.occupiedSlot)
        {
            offUserIndicator();
            return;
        }

        UpdateMainText("Get");
        if (Input.GetKeyDown(KeyCode.F))
        {
            var holderItem = itemHolder.GetComponent<IHeldItem>();
            holderItem.occupySlot("recipe", chosenRecipe.itemName, chosenRecipe.rawIcon);

            ResetBakingStation();
        }
    }

    private void ResetBakingStation()
    {
        chosenRecipe = null;
        ingredientsRequired.Clear();
        currentState = State.Bake;
    }

    public override void UseButtonFunction()
    {
        switch (currentState)
        {
            case State.Bake:
                HandleBakeState();
                break;
            case State.Process:
                HandleProcessState();
                break;
            case State.Mold:
                HandleMoldState();
                break;
            case State.Complete:
                HandleCompleteState();
                break;
        }
    }

    private void UpdateMainText(string text)
    {
        mainText.text = text;
    }
}
