using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliviaController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform[] _target;  
    private int _current;
    public float speed = 110f;  

    void Start()
    {
        _rigidbody = GetComponent < Rigidbody > ();
        GameObject path = GameObject.FindWithTag("OliviaPath");
        _target = path.GetComponentsInChildren<Transform>();
        Debug.Log(_target.Length);
    }     
    
    void Update() {
        if (GameManager.Instance.currentSceneState == GameManager.GameSceneState.PLAY)
        {
            if (Math.Abs(transform.position.x - _target[_current].position.x) > 0.1f)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, _target[_current].position,
                    speed * Time.deltaTime);
                _rigidbody.MovePosition(pos);
            }
            else
            {
                _current = (_current + 1);
                if (_current == _target.Length)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
