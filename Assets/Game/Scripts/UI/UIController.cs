using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextButton;
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    void Awake()
    {
        _nextButton.onClick.AddListener(() => LevelManager.Instance.LoadNextLevel());
        _restartButton.onClick.AddListener(() => LevelManager.Instance.RestartGame());
    }

    private void Update()
    {
        if (GameManager.Instance.currentSceneState == GameManager.GameSceneState.END)
        {
            _canvas.enabled = true;
        }
    }
}
