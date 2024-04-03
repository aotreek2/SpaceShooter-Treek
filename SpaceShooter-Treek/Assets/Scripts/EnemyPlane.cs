using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyPlane : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform fireLocation;
    private float shootDelay = 2.0f; // Delay in seconds between shots
    private float attackRange = 2;
    private float lastShotTime;
    private float speed = 20f;

    private int health;

    private Rigidbody rb;
    IObjectPool<GameObject> ammoPool;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ammoPool = FindAnyObjectByType<PoolManager>().GetComponent<PoolManager>().AmmoPool;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); 

            transform.Translate(direction * speed * Time.deltaTime);
            transform.LookAt(player);
        }
        else if(Vector3.Distance(transform.position, player.position) < attackRange)
        {
            transform.LookAt(player);

            if (Time.time - lastShotTime >= shootDelay)
            {
                    GameObject ammo = ammoPool.Get();
                    ammo.transform.position = fireLocation.transform.position;

                    ammo.GetComponent<Rigidbody>().velocity = transform.forward * 200f;
                    lastShotTime = Time.time;
 
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            health -= 20;
            Destroy(collision.gameObject);
            if(health < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
