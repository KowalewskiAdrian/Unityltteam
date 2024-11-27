using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePool : ObjectPool {
    public static PlayerProjectilePool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._playerProjectilePoolCount, _poolConfig._playerProjectilePoolPrefabs);
    }
}
