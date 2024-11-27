using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    private List<GameObject> _pooledObjects;
    private GameObject _objectToPool;
    private int _amountToPool;

    void Start() {
        _pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_objectToPool, transform);
            tmp.name = _objectToPool.name + i.ToString("D2");
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);
        }
    }

    public void SetPoolAmount(int nCount, GameObject objPoolDemo) {
        _amountToPool = nCount;
        _objectToPool = objPoolDemo;
    }

    public GameObject GetPooledObject() {
        if (_pooledObjects == null)
            return null;

        for (int i = 0; i < _amountToPool; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}
