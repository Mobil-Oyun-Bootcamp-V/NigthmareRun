using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private enum Scenes
    {
        LevelEndUI,GameStart,GameEnd,Level1,Level2,Level3
    }

    private static class ScenesHelper
    {
        public static Scenes NextLevel(Scenes currentLevel)
        {
            if (currentLevel == Scenes.GameEnd)
            {
                return Scenes.GameStart;
            }
            else if(currentLevel == Scenes.GameStart)
            {
                return Scenes.Level1;
            }
            else
            {
                bool found = false;
                foreach (Scenes scene in Enum.GetValues(typeof(Scenes)))
                {
                    if (found)
                    {
                        return scene;
                    }
                    if (currentLevel == scene)
                    {
                        found = true;
                    }
                }

                return Scenes.GameEnd;
            }
        }
    }
    
    private LevelManager(){}

    private static LevelManager _manager;

    public static LevelManager Instance => _manager;

    private void Awake()
    {
        if(_manager == null)
        {
            _manager = this;
            DontDestroyOnLoad(this);
        }
        else if (_manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadNextLevelAsync();
    }

    private Scenes _currentScene = Scenes.GameStart;
    private Scenes _nextScene;
    private AsyncOperation _async;
    private AsyncOperation _asyncDependent;

    private void LoadNextLevelAsync()
    {
        _nextScene = ScenesHelper.NextLevel(_currentScene);
        _async = SceneManager.LoadSceneAsync(_nextScene.ToString());
        _async.allowSceneActivation = false;
        if (_nextScene != Scenes.GameStart && _nextScene != Scenes.GameEnd)
        {
            _asyncDependent = SceneManager.LoadSceneAsync(Scenes.LevelEndUI.ToString(),LoadSceneMode.Additive);
            _asyncDependent.allowSceneActivation = false;
        }
        else
        {
            _asyncDependent = null;
        }
        
    }
    
    public  void StartGame()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        _currentScene = _nextScene;
        Debug.Log("NEXT LEVEL "+_currentScene);
        _async.allowSceneActivation = true;
        if (_asyncDependent != null)
        {
            _asyncDependent.allowSceneActivation = true;
        }

        GameManager.Instance.currentSceneState = GameManager.GameSceneState.PLAY;
        LoadNextLevelAsync();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Scenes.GameStart.ToString());
        Destroy(gameObject);
    }
}
