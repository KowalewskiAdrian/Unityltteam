using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupPool : ObjectPool {
    public static PlayerPowerupPool SharedInstance;
    public PoolConfig _poolConfig;

    void Awake() {
        SharedInstance = this;
        SetPoolAmount(_poolConfig._playerPowerupPoolCount, _poolConfig._playerPowerupPoolPrefabs);
    }
}
