using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public Stat maxHealth;
    public int currentHealth;

    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public virtual IEnumerator Die()
    {
        //This method is meant to be overwritten
        return null;
    }
}
