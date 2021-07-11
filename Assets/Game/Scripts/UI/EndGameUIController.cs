using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUIController : MonoBehaviour
{
    public void RestartGame()
    {
        LevelManager.Instance.RestartGame();
    }
}
