using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnPickUpPools : ObjectPool
{
    public static VFXOnPickUpPools SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.vfxOnPickupPoolCount, poolConfig.vfxOnPickupPoolPrefabs);
    }
}