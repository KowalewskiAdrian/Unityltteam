using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePool : ObjectPool
{
    public static PlayerProjectilePool SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.playerProjectilePoolCount, poolConfig.playerProjectilePoolPrefabs);
    }
}
