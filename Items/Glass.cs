using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class Glass : ScriptableObject
{
    public string itemName = "glass";
    public Sprite GlassEmpty;
    public Sprite GlassAmericano;
    public Sprite GlassSpanishLatte;
    public Sprite GlassEspresso;
}
