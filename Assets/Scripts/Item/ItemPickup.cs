using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Picked up " + item.name);
            
            bool wasPickedUp = Inventory.instance.Add(item);

            if(wasPickedUp)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
