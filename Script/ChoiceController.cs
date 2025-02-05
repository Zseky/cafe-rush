using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChoiceController : MonoBehaviour
{
    public List<GameObject> menuOptions = new List<GameObject>(); // Array to store buttons (or panels)
    private int selectedIndex = 0;
    
    public GameObject itemPrefab;
    public GameObject contentContainer;
    public Recipe[] recipeItems;

    public ScrollRect scrollRect;

    private GameObject[] panels;  // Array to hold all panels in the ScrollView

    public BakingStationScript bakingStation;

    private Color normalColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Default gray
    private Color selectedColor = new Color(0.9f, 0.9f, 0.9f, 1f);

    void Start()
    {
        //UpdateSelection();
        foreach (Recipe recipeItem in recipeItems)
        {
            CreatePanel(recipeItem);
        }

        panels = new GameObject[contentContainer.transform.childCount];
        for (int i = 0; i < panels.Length; i++) 
        {
            panels[i] = contentContainer.transform.GetChild(i).gameObject;
            SetPanelColor(panels[i], normalColor);
        }

        SetPanelColor(panels[selectedIndex], selectedColor);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveSelection(1);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerPanelAction();
        }

    }

    private void MoveSelection(int direction)
    {
        // Reset the current panel's color
        SetPanelColor(panels[selectedIndex], normalColor);

        // Update selected index
        selectedIndex = Mathf.Clamp(selectedIndex + direction, 0, panels.Length - 1);

        // Set new selected panel color
        SetPanelColor(panels[selectedIndex], selectedColor);

        // Scroll to keep selected panel in view
        ScrollToSelected();
    }

    private void ScrollToSelected()
    {
        RectTransform selectedTransform = panels[selectedIndex].GetComponent<RectTransform>();
        RectTransform contentRect = scrollRect.content;
        RectTransform viewportRect = scrollRect.viewport;

        float contentHeight = contentRect.rect.height;
        float viewportHeight = viewportRect.rect.height;
        float buttonYPos = -selectedTransform.anchoredPosition.y;

        float targetY = buttonYPos - (viewportHeight / 2) + (selectedTransform.rect.height / 2);

        targetY = Mathf.Clamp(targetY, 0, contentHeight - viewportHeight);

        contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, targetY);
    }



    private void SetPanelColor(GameObject panel, Color color)
    {
        Image img = panel.GetComponent<Image>();
        if (img != null)
        {
            img.color = color;
        }
    }

    void CreatePanel(Recipe recipe)
    {
        GameObject recipePanel = Instantiate(itemPrefab, contentContainer.transform);

        recipePanel.GetComponent<RecipeIDScript>().RecipeIDActivate(recipe);

        menuOptions.Add(recipePanel);
    }

    private void TriggerPanelAction()
    {
        foreach (Ingredients ingredient in recipeItems[selectedIndex].requiredIngredients)
        {
            bakingStation.ingredientsRequired.Add(ingredient.itemName);
        }
        bakingStation.chosenRecipe = recipeItems[selectedIndex];
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().canMove = true;
    }
    
}