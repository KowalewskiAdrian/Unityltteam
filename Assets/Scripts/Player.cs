using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour {

    public event System.Action OnDie;

    [SerializeField] private RectTransform _stickParent;
    [SerializeField] private Transform _projectileSpawnLocation;
    [SerializeField] private Transform _objAirShip;

    private int _health = 3;
    
    private Rigidbody _body = null;
    
    private bool _hasInput = false;
    
    float _fireInterval = 0.4f;
    private float _fireTimer = 0.0f;
    public float tiltAmount = 10f; // Max tilt angle
    public float tiltSpeed = 5f; // Speed of tilt adjustment
    public float stopThreshold = 0.1f;

    private Quaternion baseRotation;


    public float moveDuration = 0.3f; // Time it takes to move to a new position
    private Vector3 targetPosition;

    private void Awake() {
        _body = GetComponent<Rigidbody>();
    }

    void Start() {
        //Object.FindObjectOfType<GameplayUi>(true).UpdateHealth(_health);
        //Object.FindObjectOfType<GameOverUi>(true).Close();

        targetPosition = transform.position;
        if (_objAirShip)
            baseRotation = _objAirShip.localRotation;

    }

    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) _hasInput = true;
        if (Input.GetMouseButtonUp(0)) _hasInput = false;
        
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _fireInterval)
        {
            _fireTimer -= _fireInterval;
            if (_hasInput != true)
                return;

            GameObject objPlayerProjectile = PlayerProjectilePool.SharedInstance.GetPooledObject();
            if (objPlayerProjectile != null)
            {
                objPlayerProjectile.transform.position = _projectileSpawnLocation.position;
                objPlayerProjectile.GetComponent<Projectile>().Init(1);
                objPlayerProjectile.SetActive(true);
            }

            GameObject objVFXMuzzle = VFXMuzzlePools.SharedInstance.GetPooledObject();
            if (objVFXMuzzle != null)
            {
                objVFXMuzzle.transform.position = transform.position;
                objVFXMuzzle.SetActive(true);
            }
        }
    }

    public void Hit() {

        _health--;

        Object.FindObjectOfType<GameplayUi>(true).UpdateHealth(_health);
        GameObject objVFXOnHit = VFXOnHitPools.SharedInstance.GetPooledObject();
        if (objVFXOnHit != null)
        {
            objVFXOnHit.transform.position = transform.position;
            objVFXOnHit.SetActive(true);
        }

        if (_health <= 0) {
            GameObject objVFXExplosion = VFXExplosionPools.SharedInstance.GetPooledObject();
            if (objVFXExplosion != null)
            {
                objVFXExplosion.transform.position = transform.position;
                objVFXExplosion.SetActive(true);
            }

            Destroy(gameObject);
            OnDie?.Invoke();
            return;
        }
    }

    private void FixedUpdate() {
        // Get the local velocity of the airship
        Vector3 localVelocity = transform.InverseTransformDirection(_body.velocity);

        // Calculate the tilt based on horizontal movement (x-axis)
        float tiltSideXways = -localVelocity.x * tiltAmount;

        // Check if the airship has stopped moving
        if (Mathf.Abs(localVelocity.x) > 0 && Mathf.Abs(localVelocity.x) < stopThreshold)
        {
            // If stopped, return to upright position (no tilt)
            tiltSideXways = Mathf.Lerp(_objAirShip.eulerAngles.z, 180, Time.deltaTime * tiltSpeed);
        }

        // Create a target rotation (only z-axis changes for tilt)
        Quaternion tiltRotation = Quaternion.Euler(0, 0, tiltSideXways);
        Quaternion targetRotation = baseRotation * tiltRotation;

        // Smoothly rotate to the target tilt
        _objAirShip.rotation = Quaternion.Lerp(_objAirShip.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = context.ReadValue<Vector2>();
        Vector3 stickPosition = _stickParent.InverseTransformPoint(touchPosition);
        targetPosition = new Vector3(
            stickPosition.x / Screen.width * _stickParent.rect.width / 100.0f,
            stickPosition.y / Screen.height * _stickParent.rect.height / 100.0f + 7.0f,
            0);

        //// Tween the player to the new position
        //Tween.Position(transform, targetPosition, moveDuration, Ease.Default, 1, CycleMode.Yoyo);
        Tween.RigidbodyMovePosition(_body, targetPosition, moveDuration, Ease.OutQuad, 1, CycleMode.Yoyo);
    }

    public void AddPowerUp(PowerUp.PowerUpType type) {

        switch (type)
        {
            case PowerUp.PowerUpType.FIRE_RATE:
                _fireInterval *= 0.9f;
                break;

            case PowerUp.PowerUpType.PLAYER_HEAL:
                _health++;
                //Object.FindObjectOfType<GameplayUi>(true).UpdateHealth(_health);
                break;

            default:
                return;
        }

        GameObject objVFXOnPickUp = VFXOnPickUpPools.SharedInstance.GetPooledObject();
        if (objVFXOnPickUp != null)
        {
            objVFXOnPickUp.transform.position = transform.position;
            objVFXOnPickUp.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
            case "EnemyProjectile":
                Hit();
                other.gameObject.SetActive(false);
                break;

            case "PlayerPowerUp":
                PowerUp.PowerUpType type = other.GetComponent<PowerUp>().GetType();
                AddPowerUp(type);
                other.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }
}