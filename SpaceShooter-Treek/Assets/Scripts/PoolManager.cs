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

public class PoolManager : MonoBehaviour
{
    int maxSize = 10;
    bool collectionChecks = true;

    [SerializeField] private GameObject ammoPrefab;
    IObjectPool<GameObject> _ammoPool;

    public IObjectPool<GameObject> AmmoPool
    {
        get
        {
            if(_ammoPool == null) //if the ammopool is non existent 
            {
                _ammoPool = new ObjectPool<GameObject>(CreatePoolItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolItem, collectionChecks, 5, maxSize); //create a new object pool
            }
            return _ammoPool;
        }
    }

   GameObject CreatePoolItem()
    {
        GameObject ammo = Instantiate(ammoPrefab, transform.position, Quaternion.identity); //creates the ammo object for the pool 
        ReturnToPool returnToPool = ammo.AddComponent<ReturnToPool>(); //adds the ammo object to the return to pool script, keeps it in stock
        returnToPool.ammoPool = AmmoPool;
        return ammo;
    }

    void OnTakeFromPool(GameObject ammo)
    {
        ammo.SetActive(true); //set active when the object is ready to be taken from the pool
    }

    void OnReturnToPool(GameObject ammo)
    {
        ammo.SetActive(false); //hides the object and moves it
        ammo.transform.position = transform.position;
    }

    void OnDestroyPoolItem(GameObject ammo)
    {
        Destroy(ammo); //destroys the object
    }
}
