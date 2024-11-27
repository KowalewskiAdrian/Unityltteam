using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.ScrollRect;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable {
    public EnemyConfig enemyConfig;

    private GameController gameController;
    private Rigidbody _body;

    private int _health = 2;
    private bool _canFire = false;

    private float oscillationAmplitude;
    private float oscillationFrequency;
    private float initialX;

    private enum MovementType { Forward, Oscillating }
    private MovementType currentMovementType;

    void Awake() {
        _body = GetComponent<Rigidbody>();
    }

    void Start() {
        StartCoroutine(SpawnProjectiles());
    }

    void Update() {
        
    }

    void InitEnemy() {        
        _canFire = Random.value < 0.4f;
        _health = enemyConfig._health + Mathf.Min(Mathf.FloorToInt(Time.time / 15f), 5);

        // Initialize movement parameters
        initialX = _body.position.x;
        currentMovementType = (Random.value < 0.7f) ? MovementType.Forward : MovementType.Oscillating;

        if (currentMovementType == MovementType.Oscillating) {
            float screenWidth = GetScreenWidthInWorldUnits();
            oscillationAmplitude = Random.Range(0, screenWidth * 2 / 3);
            oscillationFrequency = Random.Range(0.5f, 1.5f); // Example frequency range
        }
    }

    IEnumerator SpawnProjectiles() {
        while (true) {
            if (_canFire){
                GameObject objEnemyProjectile = EnemyProjectilePool.SharedInstance.GetPooledObject();
                if (objEnemyProjectile != null) {
                    objEnemyProjectile.transform.position = transform.position;
                    objEnemyProjectile.SetActive(true);
                }
            }

            yield return new WaitForSeconds(enemyConfig._fireInterval);
        }
    }

    void FixedUpdate() {
        MoveForward();
    }

    void MoveForward() {
        Vector3 targetPos = _body.position;

        // Apply movement based on type
        if (currentMovementType == MovementType.Oscillating) {
            float newX = initialX + Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude;
            targetPos.x = newX;
        }

        // Move downward
        targetPos += Vector3.down * (enemyConfig._speed * Time.deltaTime);
        _body.MovePosition(targetPos);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 360f, 0) * Time.fixedDeltaTime * 0.1f);
        _body.MoveRotation(_body.rotation * deltaRotation);

        if (targetPos.y < enemyConfig._aliveLimitHeight) {
            _canFire = false;
            gameObject.SetActive(false);
        }
    }

    private float GetScreenWidthInWorldUnits() {
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            float halfHeight = mainCamera.orthographicSize;
            float halfWidth = halfHeight * mainCamera.aspect;
            return halfWidth * 2;
        }
        return 0f;
    }

    public void TakeDamage(int amount) {
        _health -= amount;

        GameObject objVFXOnHit = VFXOnHitPool.SharedInstance.GetPooledObject();
        if (objVFXOnHit != null) {
            objVFXOnHit.transform.position = transform.position;
            objVFXOnHit.SetActive(true);
        }

        if (_health <= 0) {
            GameObject objVFXExplosion = VFXExplosionPool.SharedInstance.GetPooledObject();
            if (objVFXExplosion != null) {
                objVFXExplosion.transform.position = transform.position;
                objVFXExplosion.SetActive(true);
            }

            if (Random.value < enemyConfig._powerUpSpawnChance) {
                GameObject objPowerUpProjectile = PlayerPowerupPool.SharedInstance.GetPooledObject();
                if (objPowerUpProjectile != null) {
                    objPowerUpProjectile.transform.position = new Vector3(Random.Range(-enemyConfig._offsetSpawnPowerup, enemyConfig._offsetSpawnPowerup), 17.0f, 0.0f);
                    var types = Enum.GetValues(typeof(PowerUp.PowerUpType)).Cast<PowerUp.PowerUpType>().ToList();
                    objPowerUpProjectile.GetComponent<PowerUp>().SetPowerUpType(types[Random.Range(0, types.Count)]);
                    objPowerUpProjectile.SetActive(true);
                }
            }

            EnemyOnDie();
        }

        AudioController.Instance.PlayEffect(AudioController.Instance.damageSound);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerProjectile") {
            int nDamage = other.GetComponent<Projectile>().GetDamage();
            TakeDamage(nDamage);        //GetComponent<IDamageable>().TakeDamage();
            other.gameObject.SetActive(false);
        }
    }

    public void EnemyOnPlay(GameController controller) {
        gameController = controller;
        InitEnemy();

        gameObject.SetActive(true);
    }

    public void EnemyOnDie() {
        _canFire = false;
        gameObject.SetActive(false);
        gameController.OnEnemyDie();
    }
}
