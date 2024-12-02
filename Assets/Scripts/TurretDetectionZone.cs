using System;
using UnityEngine;

public class TurretDetectionZone : MonoBehaviour
{
    
    public bool _isOn = false;

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
