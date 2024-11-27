using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "GameConfigurations/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject {
    public int _damage = 1;

    public float _aliveLimitDownHeight = -3.0f;
    public float _aliveLimitTopHeight = 17.0f;
}
