using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBars : MonoBehaviour {

    PlayerStats playerStats;

    public Slider HealthBar;
    public Text HealthText;
    public Slider ExpBar;
    public Text LevelText;
    public Text RoundText;
    public Text AliveText;
    public Text EnterText;

    // Use this for initialization
    void Start ()
    {
        playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update ()
    {
        HealthBar.maxValue = playerStats.maxHealth.GetValue();
        HealthBar.value = playerStats.currentHealth;
        ExpBar.maxValue = playerStats.expNeed;
        ExpBar.value = playerStats.exp;
        HealthText.text = playerStats.currentHealth + "/" + playerStats.maxHealth.GetValue();
        LevelText.text = "" + playerStats.lvl;
        if (SceneManager.GetActiveScene().name != "Home")
        {
            RoundText.text = "Round:" + EnemySpawner.waveIndex;
            AliveText.text = "Enemies Alive:" + EnemySpawner.EnemiesAlive;
        
            if(EnemySpawner.enemiesToSpawn == 0 && EnemySpawner.EnemiesAlive == 0)
            {
                EnterText.gameObject.SetActive(true);
            }
            else
            {
                EnterText.gameObject.SetActive(false);
            }
        }
    }
}
