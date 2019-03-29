using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

    public int ExpToGive;

    private GameObject enemyObject;

    private BaseEnemy baseEnemy;

    private Canvas enemyCanvas;

    private AudioManager audioManager;

    private void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();

        enemyObject = baseEnemy.enemyObject;

        enemyCanvas = baseEnemy.enemyCanvas;

        audioManager = AudioManager.Instance;
    }

    public override IEnumerator Die()
    {
        audioManager.Play("Death");
        PlayerManager.instance.Player.GetComponent<PlayerStats>().CheckForLevelUp(ExpToGive);     
        yield return StartCoroutine(PlayAnimation());

        enemyObject.SetActive(false);

        DropLoot();
        currentHealth = maxHealth.GetValue();        
        EnemySpawner.EnemiesAlive--;

        ResetEnemy();
    }

    public IEnumerator PlayAnimation()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if(animator == null)
        {
            print("No animator found.");
            yield break;
        }
        animator.SetTrigger("Death");
        baseEnemy.dying = true;

        GetComponent<BoxCollider>().enabled = false;
        enemyCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public void ResetEnemy()
    {
        GetComponent<BoxCollider>().enabled = true;
        enemyCanvas.gameObject.SetActive(true);
        baseEnemy.dying = false;
    }

    public void DropLoot()
    {
        DropTable table = GetComponent<DropTable>();
        int i = 0;
        foreach(LootDrop lootDrop in table.loot)
        {
            int randomNumber = Random.Range(0, 10000);
            if (lootDrop.GetWeight() >= randomNumber)
            {
                
                print(lootDrop.GetWeight() + " , " + randomNumber);
                GameObject itemToSpawn = ObjectPooler.Instance.SpawnFromPool("Drops", transform.position + new Vector3(Random.Range(-i /2, i/2), i), Quaternion.Euler(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)));
                i++;
                Item item = itemToSpawn.GetComponent<ItemPickup>().item = lootDrop.Item;
                if (item.mesh != null && item.material != null)
                {
                    itemToSpawn.GetComponent<MeshFilter>().mesh = item.mesh;
                    itemToSpawn.GetComponent<Renderer>().material = item.material;
                }
                itemToSpawn.transform.localScale = item.Size;
                itemToSpawn.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(30,80), Random.Range(300, 500), Random.Range(30, 80)));
            }
        }
    }
}
