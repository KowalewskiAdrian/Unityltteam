using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnHitPool : ObjectPool {
    public static VFXOnHitPool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._vfxOnHitPoolCount, _poolConfig._vfxOnHitPoolPrefabs);
    }
}
