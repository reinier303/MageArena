using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour {

    private bool UISetup = false;
    private bool UIOn = false;
    public GameObject MaterialPrefab;
    public CraftingRecipe craftingRecipe;

    public void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = craftingRecipe.Results[0].Item.icon;
    }

    public void OpenButton()
    {
        if (!UIOn)
        {
            SetupRecipeUI();
        }
        else
        {
            DisableRecipeUI();
        }
    }

    public void CraftButton()
    {
        craftingRecipe.Craft(Inventory.instance);
    }

    private void SetupRecipeUI()
    {
        if (UISetup == false)
        {
            foreach (ItemAmount material in craftingRecipe.Materials)
            {
                GameObject obj = Instantiate(MaterialPrefab, transform);
                obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = material.Item.icon;
                if (material.Item.stackable)
                {
                    obj.GetComponentInChildren<Text>().text = "" + material.Amount;
                }
                transform.Find("Button").SetAsLastSibling();

            }
            UISetup = true;
        }
        if (UISetup)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        UIOn = true;
    }

    private void DisableRecipeUI()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        UIOn = false;
    }
}
