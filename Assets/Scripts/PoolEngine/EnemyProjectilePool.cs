using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePool : ObjectPool {
    public static EnemyProjectilePool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._enemyProjectilePoolCount, _poolConfig._enemyProjectilePoolPrefabs);
    }
}
