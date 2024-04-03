using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject fireLocation;

    private int health = 100;
    private float speed = 60f;
    private float rotationSpeed = 120f;
    public bool gameIsOver;
    private float shootDelay = 0.5f; // Delay in seconds between shots
    private float lastShotTime;

    private Rigidbody rb;
    IObjectPool<GameObject> ammoPool;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ammoPool = FindAnyObjectByType<PoolManager>().GetComponent<PoolManager>().AmmoPool;

    }

    void Update()
    {
        if (gameIsOver == false)
        {
            Controls();
        }
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

        // moves the player forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Time.time - lastShotTime >= shootDelay)
        {
            if (Input.GetKey(KeyCode.Space))
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
        if (collision.gameObject.tag == "Laser")
        {
            health -= 20;
            Destroy(collision.gameObject);
            if (health < 0)
            {
                Destroy(this.gameObject);
            }
        }

        if (collision.gameObject.tag == "Hazard")
        {
            health -= 20;
            Destroy(collision.gameObject);
            if (health < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
