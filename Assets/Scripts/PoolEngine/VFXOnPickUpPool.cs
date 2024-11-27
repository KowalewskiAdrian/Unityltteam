using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnPickUpPool : ObjectPool {
    public static VFXOnPickUpPool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._vfxOnPickupPoolCount, _poolConfig._vfxOnPickupPoolPrefabs);
    }
}