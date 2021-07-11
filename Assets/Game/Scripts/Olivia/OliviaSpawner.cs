using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliviaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _olivia;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject oli = Instantiate(_olivia);
            oli.transform.position = new Vector3(0,0,transform.position.z - 15f);
        }
    }
}
