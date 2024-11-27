using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _gameoverUI;

    [SerializeField] private RectTransform _health;
    [SerializeField] private TMP_Text _labelPlayScore;
    [SerializeField] private TMP_Text _labelHighScore;
    [SerializeField] private TMP_Text _labelOverScore;
    [SerializeField] private GameObject _Congratulations;


    void Start() {
        
    }

    void Update() {

    }

    public void OnPlayGame(int playScore) {
        _labelPlayScore.text = playScore.ToString();

        _gameplayUI.SetActive(true);
        _gameoverUI.SetActive(false);
    }

    public void OnGameOver(int playScore, int highScore, bool congratulations) {
        _labelHighScore.text = $"High Score :{highScore.ToString()}";
        _labelOverScore.text = $"Your Score :{playScore.ToString()}";
        _Congratulations.SetActive(congratulations);

        _gameplayUI.SetActive(false);
        _gameoverUI.SetActive(true);
    }

    public void ShowPlayScore(int playScore) {
        _labelPlayScore.text = playScore.ToString();
    }

    public void ShowHealth(int health) {
        for (int i = 0; i < _health.childCount; i++)
        {
            _health.GetChild(i).gameObject.SetActive((i + 1) <= health);
        }
    }

    public void OnRetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
