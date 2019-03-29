using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerFirstPerson : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private Rigidbody rb;
    private Camera mainCam;
    PlayerStats playerStats;

    int floorMask;
    bool canShoot = true;

    ObjectPooler objectPooler;
    ProjectileTypes projectileTypes;
    PlayerManager playerManager;
    AudioManager audioManager;


    [SerializeField] public MouseLook m_MouseLook;

    public string currentProjectile = "Projectiles";

    // Use this for initialization
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = gameObject.GetComponent<Rigidbody>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        audioManager = AudioManager.Instance;
        m_MouseLook.Init(transform, mainCam.transform);
        currentProjectile = "Projectiles";
        playerManager = PlayerManager.instance;
        playerStats = PlayerStats.Instance;
        projectileTypes = ProjectileTypes.Instance;

        playerManager.Player = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Move(hAxis, vAxis);

        m_MouseLook.UpdateCursorLock();
    }
    private void Update()
    {
        RotateView();
        if (Input.GetKey("mouse 0"))
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentProjectile = projectileTypes.GetProjectileType(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentProjectile = projectileTypes.GetProjectileType(2);
        }
    }

    void Move(float h, float v)
    {
        //Vector3 direction = new Vector3(h, 0, v).normalized * playerStats.moveSpeed.GetValue() * 1000 * Time.deltaTime;
        //Vector3 direction = transform.forward * v * playerStats.moveSpeed.GetValue() * 1000 * Time.deltaTime;
        Vector3 direction = (transform.forward * v + transform.right * h) * playerStats.moveSpeed.GetValue() * 1000 * Time.deltaTime;


        rb.AddForce(direction);
    }

    IEnumerator Fire()
    {
        if (canShoot == true)
        {
            audioManager.Play("Shoot");
            Vector3 playerposition = rb.transform.position;
            if (currentProjectile == null)
            {
                currentProjectile = "Projectiles";
            }
            GameObject projectile = objectPooler.SpawnFromPool(currentProjectile, playerposition + transform.forward, rb.rotation);
            if (projectile.name == "RockProjectileContainer(Clone)")
            {
                RockProjectile rockProjectile = projectile.transform.GetChild(0).GetComponent<RockProjectile>();
                projectile.transform.localPosition += transform.forward * rockProjectile.startPos;
                StartCoroutine(rockProjectile.StopAfterTime());
            }
            canShoot = false;
            float delay = 1 / (float)(playerStats.fireRate.GetValue());
            yield return new WaitForSeconds(delay);
            canShoot = true;
        }
    }


    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, mainCam.transform);
    }
}
