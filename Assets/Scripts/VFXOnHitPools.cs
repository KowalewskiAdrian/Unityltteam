using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnHitPools : ObjectPool
{
    public static VFXOnHitPools SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.vfxOnHitPoolCount, poolConfig.vfxOnHitPoolPrefabs);
    }
}
