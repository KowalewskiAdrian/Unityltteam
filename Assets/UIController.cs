using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _gameoverUI;

    [SerializeField] private RectTransform _health;
    [SerializeField] private TMP_Text _labelPlayScore;
    [SerializeField] private TMP_Text _labelOverScore;

    private int _currentScore = 0;
    private int _currentHealth = 0;


    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        _currentHealth = 3;

        _labelPlayScore.text = _currentScore.ToString();
        _labelOverScore.text = _currentScore.ToString();
        ShowHealth();
    }

    public void ShowPlayScore()
    {
        _labelPlayScore.text = _currentScore.ToString();
    }

    public void ShowGameRetry()
    {

    }

    public void ShowHealth()
    {
        for (int i = 0; i < _health.childCount; i++)
        {
            _health.GetChild(i).gameObject.SetActive((i + 1) <= _currentHealth);
        }
    }
    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncScore() { _currentScore++; }

    public void IncHealth() { _currentHealth++; }
    public void DecHealth() { _currentHealth--; }
}
