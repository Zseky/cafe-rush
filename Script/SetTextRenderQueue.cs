using TMPro;
using UnityEngine;

public class SetTextRenderQueue : MonoBehaviour
{
    public TextMeshPro textMeshPro;  // Assign the TextMeshPro object in the inspector

    void Start()
    {
        textMeshPro= GetComponent<TextMeshPro>();

        // Get the MeshRenderer component (if applicable)
        Renderer renderer = textMeshPro.GetComponent<Renderer>();

        if (renderer != null)
        {
            // Access the material of the TMP object
            Material material = renderer.material;

            // Set the render queue to a higher value to make it render after the background
            material.renderQueue = 10000;  // Default is around 2000, you can adjust as needed
        }
    }
}