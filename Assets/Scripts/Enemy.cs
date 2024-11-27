using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

    private float _powerUpSpawnChance = 0.1f;
    private int _health = 2;
    private float _speed = 2.0f;
    private float _aliveLimitHeight = -3.0f;
    private float _offsetSpawnPowerup = 3.0f;
    private Rigidbody _body;

    private bool canFire = false;
    private float _fireInterval = 2.5f;
    private float _fireTimer = 0.0f;
    

    private void Awake() {
        _body = GetComponent<Rigidbody>();
        canFire = Random.value < 0.4f;
        _health = 2 + Mathf.Min(Mathf.FloorToInt(Time.time / 15f), 5);
    }

    void Update() {

        if (canFire) {
            _fireTimer += Time.deltaTime;
            if (_fireTimer >= _fireInterval) {
                GameObject objEnemyProjectile = EnemyProjectilePool.SharedInstance.GetPooledObject();
                if (objEnemyProjectile != null)
                {
                    objEnemyProjectile.transform.position = transform.position;
                    objEnemyProjectile.SetActive(true);
                }

                _fireTimer -= _fireInterval;
            }
        }
    }

    private void FixedUpdate() {
        var p = _body.position;
        p += Vector3.down * (_speed * Time.deltaTime);
        _body.MovePosition(p);

        if (p.y < _aliveLimitHeight)
            gameObject.SetActive(false);
    }

    public void Hit(int damage) 
    {
        _health -= damage;
        if (_health <= 0) 
        {
            GameObject objVFXExplosion = VFXExplosionPools.SharedInstance.GetPooledObject();
            if (objVFXExplosion != null) 
            {
                objVFXExplosion.transform.position = transform.position;
                objVFXExplosion.SetActive(true);
            }

            if (Random.value < _powerUpSpawnChance) 
            {
                GameObject objPowerUpProjectile = PlayerPowerupPools.SharedInstance.GetPooledObject();
                if (objPowerUpProjectile != null)
                {
                    objPowerUpProjectile.transform.position = new Vector3(Random.Range(-_offsetSpawnPowerup, _offsetSpawnPowerup), 17.0f, 0.0f);
                    var types = Enum.GetValues(typeof(PowerUp.PowerUpType)).Cast<PowerUp.PowerUpType>().ToList();
                    objPowerUpProjectile.GetComponent<PowerUp>().SetType(types[Random.Range(0, types.Count)]);
                    objPowerUpProjectile.SetActive(true);
                }
            }
            
            gameObject.SetActive(false);
            Object.FindObjectOfType<GameController>(true).OnEnemyDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile")
        {
            int nDamage = other.GetComponent<Projectile>().GetDamage();
            Hit(nDamage);
            other.gameObject.SetActive(false);
        }
    }

}
