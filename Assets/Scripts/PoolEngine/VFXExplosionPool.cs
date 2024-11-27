using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXExplosionPool : ObjectPool {
    public static VFXExplosionPool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._vfxExplosionPoolCount, _poolConfig._vfxExplosionPoolPrefabs);
    }
}
