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

public class ReturnToPool : MonoBehaviour
{
    public IObjectPool<GameObject> ammoPool;

    private void OnEnable()
    {
        Invoke("ReturnToPoolManager", 5);
    }

    private void ReturnToPoolManager()
    {
        ammoPool.Release(gameObject); //releases the pooled gameobject
    }
}
