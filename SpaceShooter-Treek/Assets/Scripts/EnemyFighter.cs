//////////////////////////////////////////////
//Assignment/Lab/Project: SpaceShooter_Treek
//Name: Ahmed Treek
//Section: SGD.213.0021
//Instructor: Aurore Locklear
//Date: 3/31/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyFighter : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform fireLocationL, fireLocationR;
    private float shootDelay = 2.0f; // Delay in seconds between shots
    private float attackRange = 50;
    private float lastShotTime;
    private float speed = 20f;
    private int health;

    [SerializeField] private SpawnManager spawnManager;
    IObjectPool<GameObject> ammoPool;
    private PlayerMovement playerM;
    private void Awake()
    {
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>(); //on awake find the spawnmanager, player script, and the ammopool
        playerM = GameObject.FindAnyObjectByType<PlayerMovement>();
        ammoPool = FindAnyObjectByType<PoolManager>().GetComponent<PoolManager>().AmmoPool;

    }
    void Start()
    {
        player = GameObject.Find("Player").transform; //finds the players transform
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > attackRange) //if the enemy is not in attack range
        {
            Vector3 direction = player.position - transform.position; //move towards the player
            direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime); //calculates, moves towards, and looks at the player
            transform.LookAt(player);
        }
        else if (Vector3.Distance(transform.position, player.position) < attackRange) //if the enemy is in attack range
        {
            transform.LookAt(player);

            if (Time.time - lastShotTime >= shootDelay)
            {
               GameObject ammoL = ammoPool.Get();   //gets two lasers from the ammo pool and shoots it out
               ammoL.transform.position = fireLocationL.transform.position;

               ammoL.GetComponent<Rigidbody>().velocity = transform.forward * 150f;

               GameObject ammoR = ammoPool.Get();
               ammoR.transform.position = fireLocationR.transform.position;

               ammoR.GetComponent<Rigidbody>().velocity = transform.forward * 150f;
               lastShotTime = Time.time;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            health -= 20; //if the enemy is killed
            Destroy(collision.gameObject);
            if (health < 0)
            {
                spawnManager.enemyCount -= 1; 
                playerM.score += 50; //add 50 points
                Destroy(this.gameObject);
            }
        }
    }
}

