using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public List<LootDrop> loot = new List<LootDrop>();
}

[System.Serializable]
public class LootDrop
{
    public Item Item;
    public float WeightPercentage;
    
    public int GetWeight()
    {
        return (int)(WeightPercentage * 100);
    }
}


