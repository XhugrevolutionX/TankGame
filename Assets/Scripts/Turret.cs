using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Turret : MonoBehaviour
{

    
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float _headRotateSpeed = 50;
    [SerializeField] private Projectile bullet;
    [SerializeField] private Transform firePoint0;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3; 
    private bool _shootOn;
    private TurretDetectionZone _turretDetectionZone = null;
    private GameObject _head;
    private float _headAngleY;
    private bool _headRotationDir = false;
    private bool _headStopRotation = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _turretDetectionZone = transform.GetChild(1).GetChild(2).GetChild(4).GetChild(0).GetComponent<TurretDetectionZone>();
        _head = transform.GetChild(1).GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_turretDetectionZone._isOn)
        {
            StartCoroutine("DetectionDelay");
            
            if(!_shootOn)
            {
                StartCoroutine("Fire");
            }
        }
        else
        {
            _shootOn = false;
            _headStopRotation = false;
            StopCoroutine("DetectionDelay");
            StopCoroutine("Fire");
        }

        if (!_headStopRotation)
        {
            //Get the Y angle of the head 
            if (_head.transform.eulerAngles.y <= 180f)
            {
                _headAngleY = _head.transform.eulerAngles.y;
            }
            else
            {
                _headAngleY = _head.transform.eulerAngles.y - 360f;
            }

            //Make the head rotate
            if (_headAngleY >= 90)
            {
                _headRotationDir = true;
            }

            if (_headAngleY <= -90)
            {
                _headRotationDir = false;
            }

            if (_headRotationDir == false)
            {
                _head.transform.Rotate(0, _headRotateSpeed * Time.deltaTime, 0);
            }
            else
            {
                _head.transform.Rotate(0, -_headRotateSpeed * Time.deltaTime, 0);
            }
        }
    }

    IEnumerator DetectionDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            
            _headStopRotation = true;
        }
    }
    
    IEnumerator Fire()
    {
        _shootOn = true;
        while (true)
        {
            Instantiate(bullet, firePoint0.position, firePoint0.rotation);
            Instantiate(bullet, firePoint1.position, firePoint1.rotation);
            Instantiate(bullet, firePoint2.position, firePoint2.rotation);
            Instantiate(bullet, firePoint3.position, firePoint3.rotation);
            yield return new WaitForSeconds(fireRate);
        }
    }

}
