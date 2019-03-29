using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject ProjectilePrefab;
    private Rigidbody rb;
    private Camera mainCam;
    PlayerStats playerStats;

    int floorMask;
    float camRayLength = 100f;
    bool canShoot = true;

    ObjectPooler objectPooler;
    ProjectileTypes projectileTypes;
    PlayerManager playerManager;
    AudioManager audioManager;


    public string currentProjectile = "Projectiles";

    // Use this for initialization
    void Awake ()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = gameObject.GetComponent<Rigidbody>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        currentProjectile = "Projectiles";
        audioManager = AudioManager.Instance;
        playerManager = PlayerManager.instance;
        playerStats = PlayerStats.Instance;
        objectPooler = ObjectPooler.Instance;
        projectileTypes = ProjectileTypes.Instance;

        playerManager.Player = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //Movement
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Move(hAxis, vAxis);
        Turning();
    }
    private void Update()
    {
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
        Vector3 direction = new Vector3(h, 0, v).normalized * playerStats.moveSpeed.GetValue() * 1000 * Time.deltaTime;

        rb.AddForce(direction);
    }

    void Turning()
    {
        Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    IEnumerator Fire()
    {
        if (canShoot == true)
        {
            audioManager.Play("Shoot");
            Vector3 playerposition = rb.transform.position;
            if(currentProjectile == null)
            {
                currentProjectile = "Projectiles";
            }
            print(objectPooler);
            GameObject projectile = objectPooler.SpawnFromPool(currentProjectile, new Vector3(playerposition.x + transform.forward.x, 0.51f, playerposition.z + transform.forward.z), rb.rotation);
            if(projectile.name == "RockProjectileContainer(Clone)")
            {
                RockProjectile rockProjectile = projectile.transform.GetChild(0).GetComponent<RockProjectile>();
                projectile.transform.localPosition +=  transform.forward * rockProjectile.startPos;
                StartCoroutine(rockProjectile.StopAfterTime());
            }
            canShoot = false;
            float delay = 1 / (float)(playerStats.fireRate.GetValue());
            yield return new WaitForSeconds(delay);
            canShoot = true;
        }
    }
}
