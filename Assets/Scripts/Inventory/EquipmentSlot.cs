using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;

    public Item item;

    public int slotNumber;
    public Sprite baseImage;

    public void ChangeEquipment(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
    }

    public void RemoveEquipment()
    {
        item = null;

        icon.sprite = baseImage;

        EquipmentManager.instance.Unequip(slotNumber);
    }

}
