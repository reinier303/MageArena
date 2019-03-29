using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour{

    public Image icon;

    public Text amount;

    public Item item;

    public GameObject confirmationScreen;

    InventoryUI inventoryUI;

    Inventory inventory;

    private void Start()
    {
        inventoryUI = transform.parent.parent.parent.GetComponent<InventoryUI>();
        inventory = Inventory.instance;
    }

    public virtual void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        if(item.stackable == true)
        {
            amount.text = "" + item.currentAmount;
        }
        else
        {
            amount.text = "";
        }
        icon.enabled = true;
    }

    public virtual void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        amount.text = "";
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

    public void RemoveItem()
    {
        inventory.Remove(item);
    }
}
