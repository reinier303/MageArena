using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject{

    public int itemID;
    public string itemName = "New Item";
    public Sprite icon = null;
    public Mesh mesh;
    public Material material;
    public Vector3 Size;
    [TextArea(15, 20)]
    public string description = "New Description";

    public bool isDefaultItem = false;

    public int maxStack = 999;
    public int currentAmount = 0;
    public bool stackable;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
     
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
