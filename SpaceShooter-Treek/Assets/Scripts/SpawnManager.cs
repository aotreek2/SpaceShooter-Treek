using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject asteroidPrefab, enemyOnePrefab, enemyTwoPrefab;
    private float spawnInterval = 3f;
    private float speed = 15f;
    private int enemyCount, asteroidCount;

    [SerializeField] private PlayerMovement playerMovement;

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnAsteroids()
    {
        while (playerMovement.gameIsOver == false && asteroidCount <= 5)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-100f, 100f), transform.position.y, 50f);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            asteroid.GetComponent<Rigidbody>().velocity = Vector3.back * speed;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnEnemies()
    {

        while (playerMovement.gameIsOver == false && enemyCount <= 3)
        {
            Vector3 spawnPosition = new Vector3(-108f, transform.position.y, Random.Range(-50f, 50f));
            GameObject enemyOne = Instantiate(enemyOnePrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(10f);
           
        }
    }
}
