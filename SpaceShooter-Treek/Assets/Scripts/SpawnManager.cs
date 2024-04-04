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

public class SpawnManager : MonoBehaviour
{
    public GameObject asteroidPrefab, enemyOnePrefab, enemyTwoPrefab, healthPickupPrefab, speedPickupPrefab;
    private float spawnInterval = 3f;
    private float speed = 15f;
    public int enemyCount, asteroidCount;

    [SerializeField] private PlayerMovement playerMovement;

    void Start()
    {
        StartCoroutine(SpawnAsteroids()); //starts the coroutines to spawn the enemies,hazards and pickups
        StartCoroutine(SpawnKamikazeEnemies());
        StartCoroutine(SpawnFighterEnemies());
        StartCoroutine(SpawnPickups());

    }




    IEnumerator SpawnAsteroids()
    {
        while (playerMovement.gameIsOver == false && asteroidCount <= 5) //while the game is not over and there are less than or equal to 5 asteroids on screen
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-100f, 100f), transform.position.y, 50f); //creates the spawn position
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity); //instantiates the asteroid
            asteroid.GetComponent<Rigidbody>().velocity = Vector3.back * speed; //adds velocity
            asteroidCount += 1; //add one everytime an asteroid is spawned
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnKamikazeEnemies()
    {

        while (playerMovement.gameIsOver == false && enemyCount <= 3) //while the game is not over and there are less than or equal to 3 enemies on screen
        {
            Vector3 spawnPosition = new Vector3(-90f, transform.position.y, Random.Range(-50f, 50f));
            GameObject enemyOne = Instantiate(enemyOnePrefab, spawnPosition, Quaternion.identity);
            enemyCount += 1; //add one everytime an enemy is spawned
            yield return new WaitForSeconds(10f);           
        }
    }

    IEnumerator SpawnFighterEnemies()
    {

        while (playerMovement.gameIsOver == false && enemyCount <= 3) //while the game is not over and there are less than or equal to 3 enemies on screen
        {
            Vector3 spawnPosition = new Vector3(85f, transform.position.y, Random.Range(-50f, 50f)); //spawns on right side
            GameObject enemyTwo = Instantiate(enemyTwoPrefab, spawnPosition, Quaternion.identity);
            enemyCount += 1;
            yield return new WaitForSeconds(12f);
        }
    }

    IEnumerator SpawnPickups()
    {

        while (playerMovement.gameIsOver == false) 
        {
            int a = Random.Range(0, 2); //random number between 0 and 1 for the switch statement, decides wether to spawn a health or speed pick up
            Vector3 spawnPosition = new Vector3(Random.Range(-100f, 100f), 0, 50f);

            switch (a)
            {
                case 0: //health pick up
                    GameObject healthPickup = Instantiate(healthPickupPrefab, spawnPosition, Quaternion.identity); //
                    healthPickup.GetComponent<Rigidbody>().velocity = Vector3.back * speed;
                    Destroy(healthPickup, 7f);
                    yield return new WaitForSeconds(12f);
                    break;
                case 1: //speed pick up
                    GameObject speedPickup = Instantiate(speedPickupPrefab, spawnPosition, Quaternion.identity);
                    speedPickup.GetComponent<Rigidbody>().velocity = Vector3.back * speed;
                    Destroy(speedPickup, 7f);
                    yield return new WaitForSeconds(12f);
                    break;
            }
           
        }
    }
}
