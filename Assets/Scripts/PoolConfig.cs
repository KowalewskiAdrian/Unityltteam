using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolConfig", menuName = "GameConfigurations/PoolConfig")]
public class PoolConfig : ScriptableObject
{
    public GameObject enemyObjectPoolPrefabs;
    public int enemyObjectPoolCount;

    public GameObject enemyProjectilePoolPrefabs;
    public int enemyProjectilePoolCount;

    public GameObject playerProjectilePoolPrefabs;
    public int playerProjectilePoolCount;

    public GameObject playerPowerupPoolPrefabs;
    public int playerPowerupPoolCount;

    public GameObject vfxExplosionPoolPrefabs;
    public int vfxExplosionPoolCount;

    public GameObject vfxOnPickupPoolPrefabs;
    public int vfxOnPickupPoolCount;

    public GameObject vfxOnHitPoolPrefabs;
    public int vfxOnHitPoolCount;

    public GameObject vfxMuzzlePoolPrefabs;
    public int vfxMuzzlePoolCount;
}
