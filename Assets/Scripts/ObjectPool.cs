using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> pooledObjects;
    private GameObject objectToPool;
    private int amountToPool;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public void SetPoolAmount(int nCount, GameObject objPoolDemo)
    {
        amountToPool = nCount;
        objectToPool = objPoolDemo;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
