using System;
using UnityEngine;

public interface IHeldItem
{

    void occupySlot(string itemName, Sprite itemSprite);
    void emptySlot();

    
}