using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GameController : MonoBehaviour {

    [SerializeField] Player _player;
    [SerializeField] UIController _uiController;
    public GameConfig gameConfig;

    private int _highScore = 0;
    private int _playScore = 0;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start() {
        _player.actionOnDie += OnPlayerDie;
        _player.actionOnHit += OnPlayerHit;

        _highScore = PlayerPrefs.GetInt("HighScore", 0);

        StartCoroutine(SpawnEnemies());

        InitPlayer();

        AudioController.Instance.PlayBackgroundMusic();
    }

    void Update() {
        
    }

    void InitPlayer() {
        Time.timeScale = 1.0f;

        _player.InitPlayer();
        _player.gameObject.SetActive(true);
        _playScore = 0;

        _uiController.OnPlayGame(_playScore);
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            GameObject objEnemyObject = EnemyObjectPool.SharedInstance.GetPooledObject();
            if (objEnemyObject != null) {
                objEnemyObject.transform.position = gameConfig._spawnPosition + new Vector3(
                    Random.Range(-gameConfig._spawnOffsets.x, gameConfig._spawnOffsets.x),
                    Random.Range(-gameConfig._spawnOffsets.y, gameConfig._spawnOffsets.y),
                    0.0f);

                Enemy enemy = objEnemyObject.GetComponent<Enemy>();
                if (enemy != null) {
                    enemy.actionOnDie = null;
                    enemy.actionOnDie += OnEnemyDie;
                    enemy.EnemyOnPlay();
                }
            }

            yield return new WaitForSeconds(gameConfig._enemySpawnInterval);
        }
    }


    void OnPlayerHit(int health) {
        _uiController.ShowHealth(health);

        AudioController.Instance.PlayEffect(AudioController.Instance.damageSound);
    }

    void OnPlayerDie() {
        Time.timeScale = 0.0f;
        bool congratulations = false;

        if (_playScore > _highScore) {
            PlayerPrefs.SetInt("HighScore", _playScore);
            PlayerPrefs.Save();

            AudioController.Instance.PlayEffect(AudioController.Instance.gameOverSoundWithHighScore);
            congratulations = true;
        }
        else
        {
            AudioController.Instance.PlayEffect(AudioController.Instance.gameOverSoundWithLowScore);
            congratulations = false;
        }

        _uiController.OnGameOver(_playScore, _highScore, congratulations);
    }

    void OnEnemyDie() {
        _playScore++;
        _uiController.ShowPlayScore(_playScore);

        AudioController.Instance.PlayEffect(AudioController.Instance.destroyEnemySound);
    }
}
