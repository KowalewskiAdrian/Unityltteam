using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "GameConfigurations/EnemyConfig")]
public class EnemyConfig : ScriptableObject {
    public int _health = 2;
    public float _speed = 2.0f;
    
    public float _aliveLimitHeight = -3.0f;

    public float _offsetSpawnPowerup = 3.0f;
    public float _powerUpSpawnChance = 0.1f;

    public float _fireInterval = 2.5f;
}
