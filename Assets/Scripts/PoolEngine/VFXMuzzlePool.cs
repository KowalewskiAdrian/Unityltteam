using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXMuzzlePool : ObjectPool {
    public static VFXMuzzlePool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._vfxMuzzlePoolCount, _poolConfig._vfxMuzzlePoolPrefabs);
    }
}
