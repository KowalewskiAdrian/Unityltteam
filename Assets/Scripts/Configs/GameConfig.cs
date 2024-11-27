using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfigurations/GameConfig")]
public class GameConfig : ScriptableObject {
    public Vector3 _spawnPosition = new Vector3(0, 16, 0);
    public Vector3 _spawnOffsets = new Vector3(2.8f, 0, 0);

    public float _enemySpawnInterval = 1.25f;
}
