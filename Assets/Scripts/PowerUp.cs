using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private float _speed = 3.0f;
    private float _aliveLimitHeight = -3.0f;

    public enum PowerUpType {
        FIRE_RATE = 1,
        PLAYER_HEAL = 2,
    }

    [SerializeField] private PowerUpType _type;


    public void SetType(PowerUpType type) {
        _type = type;
    }

    public PowerUpType GetType() { 
        return _type; 
    }

    private void Update() {
        var p = transform.position;
        p += Vector3.down * (_speed * Time.deltaTime);
        transform.position = p;

        if (p.y < _aliveLimitHeight)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        return;
        var player = other.GetComponent<Player>();
        if (player == null) return;

        player.AddPowerUp(_type);
        gameObject.SetActive(false);
    }
}
