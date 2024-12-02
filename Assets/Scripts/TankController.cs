using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TankController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _linearForce = 10;
    [SerializeField] private float _turnForce = 2;
    private float _linearMovementInput = 0;
    private float _turnMovementInput = 0;
    
    private GameObject _canon;
    [SerializeField] private float _canonTurnSpeed = 2;
    [SerializeField] private float _canonAngleSpeed = 2;
    private float _canonTurnInput = 0;
    private float _canonAngleInput = 0;
    private float _canonAngleY;
    private float _canonAngleX;
    private float _canonAngleXUpperLimit = -10;
    private float _canonAngleXLowerLimit = 0;
    
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private Projectile bullet;
    [SerializeField] private Transform firePoint;
    private Coroutine shoot_routine = null;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _canon = transform.GetChild(3).GameObject();
        if(_rigidbody == null)
        { 
            Debug.LogWarning("No rigidbody attached");
        }
        else if (_canon == null)
        {
            Debug.LogWarning("No Object n°3 attached (canon)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move the Tank
        _rigidbody.AddForce(transform.forward * (_linearForce * _linearMovementInput));
        _rigidbody.AddRelativeTorque(0, _turnForce * _turnMovementInput, 0);
        
        //Rotate the canon around the y-axis
        _canon.transform.Rotate(0, _canonTurnSpeed * _canonTurnInput * Time.deltaTime, 0);
        
        //Rotate the canon's around the x-axis if it is between the limits
        if ( _canonAngleX <= _canonAngleXLowerLimit &&  _canonAngleX >= _canonAngleXUpperLimit)
        {
            _canon.transform.Rotate(_canonAngleSpeed * _canonAngleInput * Time.deltaTime, 0, 0);
        }
        
        //Get the X angle of the Canon before rotation
        if(_canon.transform.eulerAngles.x <= 180f)
        {
            _canonAngleX = _canon.transform.eulerAngles.x;
        }
        else
        {
            _canonAngleX = _canon.transform.eulerAngles.x - 360f;
        }
        
        //Get the Y angle of the Canon 
        if(_canon.transform.eulerAngles.y <= 180f)
        {
            _canonAngleY = _canon.transform.eulerAngles.y;
        }
        else
        {
            _canonAngleY = _canon.transform.eulerAngles.y - 360f;
        }
        
        //If the canon's x-axis rotation is out of the limits put it back in
        if (_canonAngleX > _canonAngleXLowerLimit)
        {
            _canonAngleX = _canonAngleXLowerLimit;
            _canon.transform.localEulerAngles = new Vector3(_canonAngleX, _canon.transform.localEulerAngles.y, 0);
        }
        
        if (_canonAngleX < _canonAngleXUpperLimit)
        {
            _canonAngleX = _canonAngleXUpperLimit;
            _canon.transform.localEulerAngles = new Vector3(_canonAngleX, _canon.transform.localEulerAngles.y, 0);
        }
        
    }
    
    void OnMoveTank(InputValue value)
    {
        _linearMovementInput = value.Get<float>();
    }

    void OnTurnTank(InputValue value)
    {
        _turnMovementInput = value.Get<float>();
    }
    
    void OnTurnBarrel(InputValue value)
    {
        _canonTurnInput = value.Get<float>();
    }
    
    void OnAngleBarrel(InputValue value)
    {
        _canonAngleInput = value.Get<float>();
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(fireRate);
        }
    }
    
    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            if(shoot_routine != null)
            {
                StopCoroutine(shoot_routine);
            }
            shoot_routine = StartCoroutine("Fire");
        }
        else
        {
            StopCoroutine("Fire");
            shoot_routine = null;
        }
    }
}