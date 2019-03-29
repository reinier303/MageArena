using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : MonoBehaviour {

    public GameObject CraftingScreen;

    public bool inRange;

    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
        CraftingScreen.SetActive(false);
    }

    public void Update()
    {
        if(inRange && Input.GetButtonDown("Crafting"))
        {
            CraftingScreen.SetActive(!CraftingScreen.activeSelf);
            if(playerManager.FirstPerson)
            {
                playerManager.CursorLocker();
            }
        }
    }

}
