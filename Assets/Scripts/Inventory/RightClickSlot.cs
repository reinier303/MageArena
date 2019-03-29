using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RightClickSlot : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent rightClick;

    public InventorySlot inventorySlot;

    public void Start()
    {
        inventorySlot = transform.parent.GetComponent<InventorySlot>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(inventorySlot.item != null)
            {
                rightClick.Invoke();
            }
        }
    }
}
