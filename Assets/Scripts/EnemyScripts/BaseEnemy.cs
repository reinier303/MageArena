using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class BaseEnemy : MonoBehaviour {

    public bool dying;

    public float moveSpeed;
    public float currentSpeed;

    public GameObject target;

    PlayerStats playerStats;
    PlayerManager playerManager;
    CharacterStats myStats;
    CharacterCombat combat;

    Slider HealtBar;

    private NavMeshAgent agent;

    public GameObject enemyObject;

    public Canvas enemyCanvas;

    public float NewDestinationTime = 1f;
    private float NewDestinationTimer = 1f;

    void Awake()
    {
        target = PlayerManager.instance.Player;
        HealtBar = gameObject.transform.parent.GetComponentInChildren<Slider>();
    }

    private void Start()
    {

        dying = false;
        currentSpeed = moveSpeed;
        NewDestinationTimer = NewDestinationTime;

        playerStats = PlayerStats.Instance;
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
        agent = GetComponentInChildren<NavMeshAgent>();

        HealtBar.gameObject.SetActive(false);
        HealtBar.maxValue = myStats.maxHealth.GetValue();
        HealtBar.value = myStats.currentHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        NewDestinationTimer -= Time.deltaTime;
        float speed = currentSpeed * Time.deltaTime;
        if(!dying)
        {
            Move();
        }
        HealtBar.transform.position = transform.position + new Vector3(0, 2.5f, 0);
    }

    private void Move()
    {
        if (NewDestinationTimer < 0)
        {
            NewDestinationTimer = NewDestinationTime;
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if(this.gameObject == isActiveAndEnabled)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    combat.Attack(targetStats);
                }
                StartCoroutine(Knockback());
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 11)
        {
            StartCoroutine(ShowHP());
            HealtBar.value -= playerStats.damage.GetValue();
            CharacterCombat playerCombat = playerManager.Player.GetComponent<CharacterCombat>();
            if (playerCombat != null)
            {
                playerCombat.Attack(myStats);
            }
        }
    }

    IEnumerator Knockback()
    {
        currentSpeed = moveSpeed * -3;
        yield return new WaitForSeconds(0.25f);
        currentSpeed = moveSpeed;
    }

    IEnumerator ShowHP()
    {
        HealtBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        HealtBar.gameObject.SetActive(false);
    }

}
