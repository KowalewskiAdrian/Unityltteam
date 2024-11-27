using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupPools : ObjectPool
{
    public static PlayerPowerupPools SharedInstance;
    public PoolConfig poolConfig;

    void Awake()
    {
        SharedInstance = this;
        SetPoolAmount(poolConfig.playerPowerupPoolCount, poolConfig.playerPowerupPoolPrefabs);
    }
}
