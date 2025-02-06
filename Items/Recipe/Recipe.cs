using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public string itemName;
    public Sprite rawIcon;
    public Sprite cookedIcon;

    public List<Ingredients> requiredIngredients;
    
    public GameObject cookedPrefab;
}
