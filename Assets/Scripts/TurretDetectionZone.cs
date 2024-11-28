using System;
using UnityEngine;

public class TurretDetectionZone : MonoBehaviour
{
    
    public bool _isOn = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isOn = true;
        }
        else
        {
            _isOn = false;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isOn = true;
        }
        else
        {
            _isOn = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isOn = false;
    }
}
