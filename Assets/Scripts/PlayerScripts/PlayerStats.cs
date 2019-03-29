using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

    #region Singleton

    public static PlayerStats Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public float exp;
    public float expNeed;
    public int lvl;
    public Stat fireRate;
    public Stat moveSpeed;

    // Use this for initialization
    void Start ()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            armor.AddModifer(newItem.armorModifier);
            damage.AddModifer(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveModifer(oldItem.armorModifier);
            damage.RemoveModifer(oldItem.damageModifier);
        }
    }

    public void CheckForLevelUp(float experience)
    {
        exp += experience;
        if(exp >= expNeed)
        {
            exp = 0 + (exp - expNeed);
            lvl++;
            LevelUp();
            expNeed *= (1.25f + 0.01f * lvl);
            CheckForLevelUp(0);
        }
    }

    public void LevelUp()
    {
        currentHealth = maxHealth.GetValue();
    }
}
