using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUi : MonoBehaviour {

    [SerializeField] private TMP_Text _labelScore;
    [SerializeField] private RectTransform _health;

    private void Awake() {
        _labelScore.text = "0";
        UpdateHealth(0);

    }

    public void ShowScore(int _score) {
        _labelScore.text = _score.ToString();
    }

    public void UpdateHealth(int h) {
        for (int i = 0; i < _health.childCount; i++) {
            _health.GetChild(i).gameObject.SetActive((i + 1) <= h);
        }
    }
}
