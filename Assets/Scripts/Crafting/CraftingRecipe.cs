using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount
{
    public Item Item;
    [Range(1, 9999)]
    public int Amount;
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(Inventory inventory)
    {
        int MaterialsGot = 0;
        foreach(ItemAmount material in Materials)
        {
            if (inventory.ContainsItem(material.Item, material.Amount))
            {
                MaterialsGot++;
            }
        }
        if(MaterialsGot == Materials.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Craft(Inventory inventory)
    {
        //Check if item is craftable with current materials in inventory
        if(CanCraft(inventory))
        {
            //Remove all materials used
            foreach(ItemAmount material in Materials)
            {
                //Stackable items
                if (material.Item.stackable)
                {
                    material.Item.currentAmount -= material.Amount;
                    if(material.Item.currentAmount == 0)
                    {
                        inventory.Remove(material.Item);
                    }
                }
                //Unstackable items
                else
                {
                    inventory.Remove(material.Item);
                }
            }

            //Add result to inventory
            foreach(ItemAmount result in Results)
            {
                //Stackable items
                if (result.Item.stackable)
                {
                    if (result.Item.maxStack < result.Item.currentAmount)
                    {
                        result.Item.currentAmount += result.Amount;
                    }
                    if(result.Item.currentAmount > result.Item.maxStack)
                    {
                        result.Item.currentAmount = result.Item.maxStack;
                    }
                }
                //Unstackable items
                else
                {
                    inventory.Add(result.Item);
                }
                Debug.Log(result.Item + " Crafted");
            }
        }
        else
        {
            Debug.Log("Can't craft this item");
        }
    }
}
