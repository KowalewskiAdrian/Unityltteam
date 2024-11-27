using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePool : ObjectPool
{
    public static EnemyProjectilePool SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.enemyProjectilePoolCount, poolConfig.enemyProjectilePoolPrefabs);
    }
}
