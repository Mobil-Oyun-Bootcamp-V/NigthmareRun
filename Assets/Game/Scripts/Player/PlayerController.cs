using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private float _fingerDownX;
    private float _fingerUpX;
    private Vector3 _slideStartPos;
    private bool _sliding;
    private Camera _mainCam;
    private int _score;
    private int _count = 0;
    [SerializeField] private float _controlSensivity = 8f;
    [SerializeField] private float _pathWidth;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreText.text = "Score : " + _score;
        }
    }
    
    private void Start()
    {
        _mainCam = Camera.main;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.currentSceneState == GameManager.GameSceneState.PLAY)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 pointUp = _mainCam.ScreenToViewportPoint(touch.position);
                    _fingerUpX = pointUp.x;
                    _fingerDownX = pointUp.x;
                    _slideStartPos = transform.localPosition;
                    _sliding = true;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 pointDown = _mainCam.ScreenToViewportPoint(touch.position);
                    _fingerDownX = pointDown.x;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _sliding = false;
                    Vector3 pointDown = _mainCam.ScreenToViewportPoint(touch.position);
                    _fingerDownX = pointDown.x;
                }

                if (_sliding)
                {
                    float distanceX = _fingerDownX - _fingerUpX;
                    Vector3 pos = transform.localPosition;
                    float posX = _slideStartPos.x + (distanceX * _controlSensivity);
                    pos.x = posX;
                    pos.x = Mathf.Clamp(pos.x, -_pathWidth, _pathWidth);
                    transform.localPosition = pos;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameManager.Instance.currentSceneState == GameManager.GameSceneState.PLAY)
        {
            if (++_count == 3)
            {
                GameManager.Instance.currentSceneState = GameManager.GameSceneState.END;
                _animator.SetTrigger("Death");
            }
            else
            {
                if (transform.position.x > other.transform.position.x)
                {
                    _animator.SetTrigger("BumpLeft");
                }
                else
                {
                    _animator.SetTrigger("BumpRight");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Finish"))
        {
            GameManager.Instance.currentSceneState = GameManager.GameSceneState.END;
        }
    }
}
