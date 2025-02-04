using Unity.VisualScripting;
using UnityEngine;

public class ObjectScript : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public GameObject UseIndicator;
    public bool playerChecker = false;
    public virtual void onUserIndicator()
    {
        UseIndicator.SetActive(true);
    }
    public virtual void offUserIndicator()
    {
        UseIndicator.SetActive(false);
        
    }

    public void Interact()
    {
        UseButtonFunction();
    }
    public virtual void UseButtonFunction()
    {
       
    }


}
