using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryUI : MonoBehaviour {

    public GameObject CharacterScreen;

    public Transform itemsParent;

    public Inventory inventory;

    InventorySlot[] slots;

    PlayerManager playerManager;

    float LookSensitivity;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        playerManager = PlayerManager.instance;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            OpenInventory();
        }
    }

    void OpenInventory()
    {
        CharacterScreen.SetActive(!CharacterScreen.activeSelf);
        if(playerManager.FirstPerson)
        {
            playerManager.CursorLocker();
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
