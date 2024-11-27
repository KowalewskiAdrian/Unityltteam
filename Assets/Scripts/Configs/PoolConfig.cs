using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolConfig", menuName = "GameConfigurations/PoolConfig")]
public class PoolConfig : ScriptableObject {
    public GameObject _enemyObjectPoolPrefabs;
    public int _enemyObjectPoolCount;

    public GameObject _enemyProjectilePoolPrefabs;
    public int _enemyProjectilePoolCount;

    public GameObject _playerProjectilePoolPrefabs;
    public int _playerProjectilePoolCount;

    public GameObject _playerPowerupPoolPrefabs;
    public int _playerPowerupPoolCount;

    public GameObject _vfxExplosionPoolPrefabs;
    public int _vfxExplosionPoolCount;

    public GameObject _vfxOnPickupPoolPrefabs;
    public int _vfxOnPickupPoolCount;

    public GameObject _vfxOnHitPoolPrefabs;
    public int _vfxOnHitPoolCount;

    public GameObject _vfxMuzzlePoolPrefabs;
    public int _vfxMuzzlePoolCount;
}
