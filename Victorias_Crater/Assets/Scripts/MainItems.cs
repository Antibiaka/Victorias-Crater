using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")] 
public class MainItems : ScriptableObject
{
    new public string name = "New  Item";
    public Sprite icon = null;

    public bool isResoure = false;
    public bool isTool = false;

    
    public virtual void Use() {
        //using items
    }

}
