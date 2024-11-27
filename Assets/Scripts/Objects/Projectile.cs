using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public ProjectileConfig projectileConfig;
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private Vector3 _direction = Vector3.up;
    private int _damage = 1;

    public void SetDamage(int damage) { _damage = damage; }
    public int GetDamage() { return _damage; }

    private void Start() {
        _damage = projectileConfig._damage;
    }

    void Update() {

    }

    void FixedUpdate() {
        MoveForward(_direction, _speed, projectileConfig._aliveLimitDownHeight, projectileConfig._aliveLimitTopHeight);
    }

    void MoveForward(Vector3 direction, float speed, float downLimit, float topLimit) {
        Vector3 targetPosision = transform.position;
        targetPosision += direction * (speed * Time.deltaTime);
        transform.position = targetPosision;

        if (targetPosision.y < downLimit || targetPosision.y > topLimit)
            gameObject.SetActive(false);
    }
}
