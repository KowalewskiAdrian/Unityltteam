using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXExplosionPools : ObjectPool
{
    public static VFXExplosionPools SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.vfxExplosionPoolCount, poolConfig.vfxExplosionPoolPrefabs);
    }
}
