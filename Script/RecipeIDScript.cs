using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class RecipeIDScript:MonoBehaviour
{
    public TextMeshProUGUI recipeName;
    public TextMeshProUGUI[] recipeIngredientNames;
    public List<string> ingredientNames;
    public void RecipeIDActivate(Recipe recipe)
    {
        recipeName.text = recipe.itemName;
        foreach (Ingredients ingredient in recipe.requiredIngredients)
        {
            ingredientNames.Add(ingredient.itemName);
        }

        while (ingredientNames.Count < 4)
        {
            ingredientNames.Add(null);
        }

        for (int i = 0; i < ingredientNames.Count; i++)
        {
            if (ingredientNames[i] != null)
                recipeIngredientNames[i].text = ingredientNames[i];
        }
        
        
    }

}
