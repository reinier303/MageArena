using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypes : MonoBehaviour{

    #region Singleton

    public static ProjectileTypes Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public string GetProjectileType(int key)
    {
        if(key == 1)
        {
            return "Projectiles";
        }
        if (key == 2)
        {
            return "RockProjectiles";
        }
        return null;
    }
}
