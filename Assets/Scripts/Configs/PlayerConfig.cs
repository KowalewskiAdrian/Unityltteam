using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "GameConfigurations/PlayerConfig")]
public class PlayerConfig : ScriptableObject {
    public int _health = 3;
    
    public float tiltAmount = 10f;
    public float tiltSpeed = 5f;

    public float stopThreshold = 0.1f;
    public float moveDuration = 0.3f;

    public float _fireInterval = 0.4f;

    public int _damageMin = 1;
    public int _damageMax = 3;

    public int _hitDamage = 1;
}
