using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXMuzzlePools : ObjectPool
{
    public static VFXMuzzlePools SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.vfxMuzzlePoolCount, poolConfig.vfxMuzzlePoolPrefabs);
    }
}
