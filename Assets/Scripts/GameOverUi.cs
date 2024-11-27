using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUi : MonoBehaviour {

    [SerializeField] private TMP_Text _labelScore;

    public void Open(int _score) {
        _labelScore.text = "Your Score :" + _score.ToString();
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public void OnRetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
