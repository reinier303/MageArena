using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : ProjectileScript
{
    private GameObject indicator;
    public float AliveTime = 5;
    public float startPos;

    public override void Awake()
    {
        base.Awake();
        indicator = transform.parent.gameObject;
        indicator.transform.localScale = new Vector3(0.25f,1, (AliveTime * shotSpeed) / 9.75f);
        startPos = (AliveTime * shotSpeed) / 2.15f;
    }

    public override void Update()
    {
        rb.velocity = (transform.forward * shotSpeed);
    }

    public override void OnTriggerEnter(Collider collision)
    {

    }

    public IEnumerator StopAfterTime()
    {
        yield return new WaitForSeconds(AliveTime);
        transform.localPosition = new Vector3(0, 0, -5);
        indicator.SetActive(false);
    }
}
