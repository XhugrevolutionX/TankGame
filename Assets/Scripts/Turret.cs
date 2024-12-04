using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Turret : MonoBehaviour
{
    private TurretDetectionZone _turretDetectionZone = null;
    
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private Projectile bullet;
    [SerializeField] private Transform firePoint0;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    private bool shoot_on;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _turretDetectionZone = transform.GetChild(1).transform.GetChild(2).transform.GetChild(4).transform.GetChild(0).GetComponent<TurretDetectionZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_turretDetectionZone._isOn)
        {
            if(!shoot_on)
            {
                StartCoroutine("Fire");
            }
        }
        else
        {
            shoot_on = false;
            StopCoroutine("Fire");
        }
    }
    
    IEnumerator Fire()
    {
        shoot_on = true;
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
