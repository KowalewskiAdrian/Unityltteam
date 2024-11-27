using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "GameConfigurations/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float playerSpeed;
    public int maxHealth;
}
