using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public float shotSpeed;
    public float aliveTime;
    public Rigidbody rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

	public virtual void Update ()
    {
        rb.velocity = (transform.forward * shotSpeed);
        aliveTime -= Time.deltaTime;
        if(aliveTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        gameObject.SetActive(false);
    }
}
