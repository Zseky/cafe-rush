using UnityEngine;
using TMPro;

public class EmptyTableScript : ObjectScript
{
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private SpriteRenderer tableHolderSprite;
    [SerializeField] private TextMeshPro indicatorText;

    private ItemHolderScript occupiedStatus;

    private string tableItemType;
    private string tableItemName;
    private Sprite tableItemSprite;

    void Start()
    {
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
        occupiedStatus = itemHolder.GetComponent<ItemHolderScript>();
    }

    public override void UseButtonFunction()
    {
        if (occupiedStatus.occupiedSlot)
        {
            HandleOccupiedSlot();
        }
        else
        {
            HandleEmptySlot();
        }

        tableHolderSprite.sprite = tableItemSprite;
    }

    private void HandleOccupiedSlot()
    {
        if (tableItemName != null)
        {
            ActivateIndicator("Swap");
            if (Input.GetKeyDown(KeyCode.F))
            {
                SwapItem();
            }
        }
        else
        {
            ActivateIndicator("Put");
            if (Input.GetKeyDown(KeyCode.F))
            {
                PutItem();
            }
        }
    }

    private void HandleEmptySlot()
    {
        if (tableItemName != null)
        {
            ActivateIndicator("Get");
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetItem();
            }
        }
        else
        {
            offUserIndicator();
        }
    }

    private void SwapItem()
    {
        IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();

        // Store current table item temporarily
        var tempType = tableItemType;
        var tempName = tableItemName;
        var tempSprite = tableItemSprite;

        // Swap the table item with the held item
        tableItemType = occupiedStatus.occupyType;
        tableItemName = occupiedStatus.occupyName;
        tableItemSprite = occupiedStatus.occupySprite;

        holderItem.occupySlot(tempType, tempName, tempSprite);
    }

    private void PutItem()
    {
        IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();

        tableItemType = occupiedStatus.occupyType;
        tableItemName = occupiedStatus.occupyName;
        tableItemSprite = occupiedStatus.occupySprite;

        holderItem.emptySlot();
    }

    private void GetItem()
    {
        IHeldItem holderItem = itemHolder.GetComponent<IHeldItem>();
        holderItem.occupySlot(tableItemType, tableItemName, tableItemSprite);

        // Clear the table after getting the item
        tableItemType = null;
        tableItemName = null;
        tableItemSprite = null;
    }

    private void ActivateIndicator(string action)
    {
        indicatorText.text = action;
        onUserIndicator();
    }
}