using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField] private MeshRenderer m_MeshRenderer;

    public enum PowerUpType {
        FIRE_RATE = 1,
        PLAYER_HEAL = 2,
    }

    private PowerUpType _type;

    public void SetPowerUpType(PowerUpType type) {
        _type = type;

        switch (type) {
            case PowerUpType.FIRE_RATE:
                m_MeshRenderer.material.color = Color.white;
                break;
            case PowerUpType.PLAYER_HEAL:
                m_MeshRenderer.material.color = Color.red;
                break;
            default:
                break;
        }
    }

    public PowerUpType GetPowerUpType() { return _type; }
}
