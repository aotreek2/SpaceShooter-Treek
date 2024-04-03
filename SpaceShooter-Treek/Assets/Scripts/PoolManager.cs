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
            if(_ammoPool == null)
            {
                _ammoPool = new ObjectPool<GameObject>(CreatePoolItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolItem, collectionChecks, 5, maxSize);
            }
            return _ammoPool;
        }
    }

   GameObject CreatePoolItem()
    {
        GameObject ammo = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        ReturnToPool returnToPool = ammo.AddComponent<ReturnToPool>();
        returnToPool.ammoPool = AmmoPool;
        return ammo;
    }

    void OnTakeFromPool(GameObject ammo)
    {
        ammo.SetActive(true);
    }

    void OnReturnToPool(GameObject ammo)
    {
        ammo.SetActive(false);
        ammo.transform.position = transform.position;
    }

    void OnDestroyPoolItem(GameObject ammo)
    {
        Destroy(ammo);
    }
}
