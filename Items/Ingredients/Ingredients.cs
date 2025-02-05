using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Cooking/Ingredients")]
public class Ingredients : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
}
