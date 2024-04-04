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


public class EnemyPlane : MonoBehaviour
{
    private Transform player;
    private float speed = 35f;

    private int health;

    private Rigidbody rb;
    private SpawnManager spawnManager;
    private PlayerMovement playerM;

    private void Awake()
    {
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>(); //on awake find the spawn manager
        playerM = GameObject.FindAnyObjectByType<PlayerMovement>(); //on awake find the player script component
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform; //finds the players position

    }

    // Update is called once per frame
    void Update()
    {
      
            Vector3 direction = player.position - transform.position; //sets the direction to the players position from the enemies position
            direction.Normalize(); 

            transform.Translate(direction * speed * Time.deltaTime); //calculates and translates the enemy towards the player
            transform.LookAt(player); //makes the enemy face the player
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            health -= 20;
            Destroy(collision.gameObject);
            if(health < 0)
            {
                spawnManager.enemyCount -= 1;
                playerM.score += 50; //add score whenerver the enemy is shot with a laser
                Destroy(this.gameObject);
            }
        }
    }
}
