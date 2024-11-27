using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : ObjectPool
{
    public static EnemyObjectPool SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.enemyObjectPoolCount, poolConfig.enemyObjectPoolPrefabs);
    }
}
