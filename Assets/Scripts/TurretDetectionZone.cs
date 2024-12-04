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
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isOn = false;
        }
    }
}
