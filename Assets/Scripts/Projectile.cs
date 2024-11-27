using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private Vector3 _direction = Vector3.up;
    private int _damage = 1;
    private float _aliveLimitHeightEnemy = -3.0f;
    private float _aliveLimitHeightPlayer = 17.0f;

    public void Init(int damage) {
        _damage = damage;
    }

    public int GetDamage()
    {
        return _damage;
    }

    void Update() {

        var p = transform.position;
        p += _direction * (_speed * Time.deltaTime);
        transform.position = p;

        if (p.y < _aliveLimitHeightEnemy || p.y > _aliveLimitHeightPlayer)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        return;
        //Debug.Log(gameObject.tag + " : " + other.tag);

        bool destroy = false;
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null) {

            enemy.Hit(_damage);
            destroy = true;
        }
        else {
            var player = other.GetComponent<Player>();
            if (player != null) {

                player.Hit();
                destroy = true;
            }
        }

        if (destroy) {
            gameObject.SetActive(false);
        }
    }
}
