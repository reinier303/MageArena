using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageSpreader : MonoBehaviour
{
    public GameObject area;

    public int vegetationAmount = 500;

    ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpreadFoliage());
    }

    IEnumerator SpreadFoliage()
    {
        yield return new WaitForSeconds(0.01f);
        objectPooler = ObjectPooler.Instance;
        for (int i = 0; i <= vegetationAmount; i++)
        {
            objectPooler.SpawnFromPool("Vegetation", new Vector3(Random.Range(area.transform.localScale.x/2, -area.transform.localScale.x / 2), 0, Random.Range(area.transform.localScale.y / 2, -area.transform.localScale.y / 2)), Quaternion.identity);
        }
    }
}
