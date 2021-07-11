using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    [SerializeField] private float _speed = 3.0f;
    void Update()
    {
        if (GameManager.Instance.currentSceneState == GameManager.GameSceneState.PLAY)
        {
            transform.position += Vector3.forward * (Time.deltaTime * _speed);
        }
    }
}
