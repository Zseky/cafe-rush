using System;
using UnityEngine;

public interface IHeldItem
{

    void occupySlot(string itemType, string itemName, Sprite itemSprite);
    void emptySlot();

    
}