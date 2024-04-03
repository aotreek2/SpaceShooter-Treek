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
        ammoPool.Release(gameObject);
    }
}
