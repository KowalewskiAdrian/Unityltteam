using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : ObjectPool {
    public static EnemyObjectPool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._enemyObjectPoolCount, _poolConfig._enemyObjectPoolPrefabs);
    }
}
