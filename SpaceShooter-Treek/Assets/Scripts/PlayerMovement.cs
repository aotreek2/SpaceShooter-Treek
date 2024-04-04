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
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
   

    private int health;
    public int score;
    private float speed = 50f;
    private float rotationSpeed = 160f;
    public bool gameIsOver;
    private float shootDelay = 0.5f; // Delay in seconds between shots
    private float lastShotTime;

    [SerializeField] private AudioSource laser;
    [SerializeField] private AudioSource explosion;
    [SerializeField] private AudioSource pickup;
    [SerializeField] private GameObject fireLocation;
    [SerializeField] private TMP_Text healthTxt, speedTxt, scoreTxt;

    private Rigidbody rb;
    IObjectPool<GameObject> ammoPool;

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private UIController uiController;

 
    private void Awake()
    {
        ammoPool = FindAnyObjectByType<PoolManager>().GetComponent<PoolManager>().AmmoPool; //on awake, finds the pool manager for the ammo object
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>(); //on awake finds the spawn manager
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //get the rigidbody, set health, and set UI on screen
        health = 100;
        UpdateUI();

    }

    void Update()
    {
        if (gameIsOver == false) //if game is over disable controls
        {
            Controls();
        }

        UpdateUI(); // update UI
    }

    private void Controls()
    {
        // rotates the player left or right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // moves the player forward or backwards
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * speed;
        }
        else //stops the player 
        {
            rb.velocity = Vector3.zero;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //quit the game if the player presses escape
        }


        if (Time.time - lastShotTime >= shootDelay) // calculates the time between in game, the last time the player shot, and checks if its greater or equal to the shot delay
        {
            if (Input.GetKey(KeyCode.Space)) //when the player presses space shoots out a laser from the objecy pool
            {
                GameObject ammo = ammoPool.Get();
                ammo.transform.position = fireLocation.transform.position;

                ammo.GetComponent<Rigidbody>().velocity = transform.forward * 200f;
                laser.Play();
                lastShotTime = Time.time; //sets the last shot time to when the player shot
            }
        }
    }

    
    private void UpdateUI()
    {
        healthTxt.text = "Health: " + health; //updates the health,speed, and score
        speedTxt.text = "Speed: " + speed;
        scoreTxt.text = "Score: " + score;
    }

    private void BackToMenu()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("EndingScreen"); //if the player dies go to the ending screen
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            health -= 20; //when the player collides with a laser take 20 health
            BackToMenu();
            Destroy(collision.gameObject); //destroys the laser
            explosion.Play(); //players the explosion sfx
        }

        if (collision.gameObject.tag == "Hazard")
        {
            health -= 20; //when the player collides with a hazard take 20 health
            BackToMenu();
            Destroy(collision.gameObject);
            spawnManager.asteroidCount -= 1; //takes away an asteroid
            explosion.Play();
        }

        if (collision.gameObject.tag == "kamikaze")
        {
            health -= 50; //when the yellow ship hits the player take 50 health
            BackToMenu();
            Destroy(collision.gameObject);
            spawnManager.enemyCount -= 1; //reduces the amount of enemies on screen
            explosion.Play();
        }

        if (collision.gameObject.tag == "PickUp")
        {
            if (collision.gameObject.name == "Health(Clone)")
            {
                health += 20; //add health on pick up
                pickup.Play();
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.name == "Speed(Clone)")
            {
                speed += 5; //add speed and rotation speed on pickup
                rotationSpeed += 5;
                pickup.Play();
                Destroy(collision.gameObject);
            }
        }
    }
}

