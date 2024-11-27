using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameController : MonoBehaviour {

    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private Vector3 _spawnOffsets;
    [SerializeField] private float _enemySpawnInterval = 0.5f;
    private float _enemySpawnTimer = 0.0f;
    bool _running = true;
    int _score = 0;

    private Player _player;
    
    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start() {
        _player = Object.FindObjectOfType<Player>(true);
        _player.OnDie += OnPlayerDie;

        _running = true;
        _score = 0;
        Time.timeScale = 1.0f;
    }

    void Update() {
        if (!_running) return;
        _enemySpawnTimer += Time.deltaTime;
        if ( _enemySpawnTimer >= _enemySpawnInterval ) {
            GameObject objEnemyObject = EnemyObjectPool.SharedInstance.GetPooledObject();
            if (objEnemyObject != null)
            {
                objEnemyObject.transform.position = _spawnPosition + new Vector3(
                    Random.Range(-_spawnOffsets.x, _spawnOffsets.x),
                    Random.Range(-_spawnOffsets.y, _spawnOffsets.y),
                    0.0f);
                objEnemyObject.SetActive(true);
            }
            _enemySpawnTimer -= _enemySpawnInterval;
        }
    }

    void OnPlayerDie() {
        //Object.FindObjectOfType<GameOverUi>(true).Open(_score);
        _running = false;
        Time.timeScale = 0.0f;
    }

    public void OnEnemyDie() {
        _score++;
        //Object.FindObjectOfType<GameplayUi>(true).ShowScore(_score);
    }
}
