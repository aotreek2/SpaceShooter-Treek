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

public class Asteroid : MonoBehaviour
{
     private SpawnManager spawnManager;
     private PlayerMovement playerM;

    private void Awake()
    {
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        playerM = GameObject.FindAnyObjectByType<PlayerMovement>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            spawnManager.asteroidCount -= 1; //if the asteroid hits the death zone, destroy it 
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Laser")
        {
            spawnManager.asteroidCount -= 1;
            Destroy(collision.gameObject); //if the asteroid hits a laser, destroy it 
            Destroy(gameObject);
        }
    }
}
