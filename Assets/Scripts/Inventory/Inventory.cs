using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough inventory space");
                return false;
            }

            if (item.stackable)
            {
                if (item.currentAmount < item.maxStack)
                {
                    if(!items.Contains(item))
                    {
                        items.Add(item);
                    }
                    if (item.currentAmount == 0)
                    {
                        item.currentAmount++;                       
                    }
                    else
                    {
                        item.currentAmount++;
                    }
                }
            }
            else
            {
                items.Add(item);
            }

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public bool ContainsItem(Item item, int amount)
    {
        if(items.Contains(item) && item.currentAmount >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
