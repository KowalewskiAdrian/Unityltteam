using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour, IDamageable {

    public PlayerConfig playerConfig;

    [SerializeField] private GameController gameController;
    [SerializeField] private RectTransform _stickParent;
    [SerializeField] private Transform _projectileSpawnLocation;
    [SerializeField] private Transform _objAirShip;
        
    private Rigidbody _body = null;
    private bool _hasInput = false;

    private Quaternion baseRotation;
    private Vector3 targetPosition;

    private float _fireTimer = 0.0f;
    private float _fireInterval = 0.4f;
    private int _health = 0;

    private void Awake() {
        _body = GetComponent<Rigidbody>();
    }

    void Start() {
        InitPlayer();

        StartCoroutine(SpawnProjectiles());
    }

    public void InitPlayer() {
        _health = playerConfig._health;
        _fireInterval = playerConfig._fireInterval;

        transform.position = Vector3.zero;
        targetPosition = transform.position;
        if (_objAirShip)
            baseRotation = _objAirShip.localRotation;
    }

    void Update() {
        
    }

    IEnumerator SpawnProjectiles()
    {
        while (true) {
            if (_hasInput == true) {
                GameObject objPlayerProjectile = PlayerProjectilePool.SharedInstance.GetPooledObject();
                if (objPlayerProjectile != null) {
                    objPlayerProjectile.transform.position = _projectileSpawnLocation.position;
                    objPlayerProjectile.GetComponent<Projectile>().SetDamage(Random.Range(playerConfig._damageMin, playerConfig._damageMax));
                    objPlayerProjectile.SetActive(true);
                }

                GameObject objVFXMuzzle = VFXMuzzlePool.SharedInstance.GetPooledObject();
                if (objVFXMuzzle != null) {
                    objVFXMuzzle.transform.position = transform.position;
                    objVFXMuzzle.SetActive(true);
                }
            }

            yield return new WaitForSeconds(_fireInterval);
        }
    }

    void FixedUpdate() {
        RotateAirShip(playerConfig.tiltAmount, playerConfig.tiltSpeed, playerConfig.stopThreshold);
    }

    void RotateAirShip(float tiltAmount, float tiltSpeed, float stopThreshold) {
        // Get the local velocity of the airship
        Vector3 localVelocity = transform.InverseTransformDirection(_body.velocity);

        // Calculate the tilt based on horizontal movement (x-axis)
        float tiltSideways = -localVelocity.x * tiltAmount;

        // Check if the airship has stopped moving
        if (Mathf.Abs(localVelocity.x) > 0 && Mathf.Abs(localVelocity.x) < stopThreshold) {
            // If stopped, return to upright position (no tilt)
            tiltSideways = Mathf.Lerp(_objAirShip.eulerAngles.z, 180, Time.deltaTime * tiltSpeed);
        }

        // Create a target rotation (only z-axis changes for tilt)
        Quaternion tiltRotation = Quaternion.Euler(0, 0, tiltSideways);
        Quaternion targetRotation = baseRotation * tiltRotation;

        // Smoothly rotate to the target tilt
        _objAirShip.rotation = Quaternion.Lerp(_objAirShip.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    public void OnMove(InputAction.CallbackContext context) {
        Vector2 touchPosition = context.ReadValue<Vector2>();
        Vector3 stickPosition = _stickParent.InverseTransformPoint(touchPosition);
        targetPosition = new Vector3(
            stickPosition.x / Screen.width * _stickParent.rect.width / 100.0f,
            stickPosition.y / Screen.height * _stickParent.rect.height / 100.0f + 7.0f,
            0);

        //// Tween the player to the new position
        Tween.RigidbodyMovePosition(_body, targetPosition, playerConfig.moveDuration, Ease.OutQuad, 1, CycleMode.Yoyo);
    }

    public void OnPrimaryAction(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            _hasInput = true;
        }
        else if (context.phase == InputActionPhase.Canceled) {
            _hasInput = false;
        }
    }

    public void AddPowerUp(PowerUp.PowerUpType type) {

        switch (type) {
            case PowerUp.PowerUpType.FIRE_RATE:
                _fireInterval *= 0.9f;
                break;

            case PowerUp.PowerUpType.PLAYER_HEAL:
                _health++;
                gameController.OnPlayerHit(_health);
                break;

            default:
                return;
        }

        GameObject objVFXOnPickUp = VFXOnPickUpPool.SharedInstance.GetPooledObject();
        if (objVFXOnPickUp != null) {
            objVFXOnPickUp.transform.position = transform.position;
            objVFXOnPickUp.SetActive(true);
        }

        AudioController.Instance.PlayEffect(AudioController.Instance.healingPickupSound);
    }

    public void TakeDamage(int amount) {
        _health--;
        
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

            Destroy(gameObject);
            gameController.OnPlayerDie();
            return;
        }

        gameController.OnPlayerHit(_health);
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Enemy":
            case "EnemyProjectile":
                TakeDamage(playerConfig._hitDamage);
                other.gameObject.SetActive(false);
                break;

            case "PlayerPowerUp":
                PowerUp.PowerUpType type = other.GetComponent<PowerUp>().GetPowerUpType();
                AddPowerUp(type);
                other.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }
}