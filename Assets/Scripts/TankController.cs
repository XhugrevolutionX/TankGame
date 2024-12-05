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
    private GameObject _head;
    [SerializeField] private float _canonTurnSpeed = 2;
    [SerializeField] private float _headAngleSpeed = 2;
    private float _canonTurnInput = 0;
    private float _headAngleInput = 0;
    private float _canonAngleY;
    private float _headAngleX;
    private float _headAngleXUpperLimit = -10;
    private float _headAngleXLowerLimit = 0;
    
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private Projectile bullet;
    [SerializeField] private Transform firePoint;
    private Coroutine shoot_routine = null;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _head = transform.GetChild(3).GetChild(0).GameObject();
        _canon = transform.GetChild(3).GameObject();
        if(_rigidbody == null)
        { 
            Debug.LogWarning("No rigidbody attached");
        }
        else if (_canon == null)
        {
            Debug.LogWarning("No Object attached (canon)");
        }
        else if (_head == null)
        {
            Debug.LogWarning("No Object attached (head)");
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
        
        //Rotate the head around the x-axis if it is between the limits
        if ( _headAngleX <= _headAngleXLowerLimit &&  _headAngleX >= _headAngleXUpperLimit)
        {
            _head.transform.Rotate(_headAngleSpeed * _headAngleInput * Time.deltaTime, 0, 0);
        }
        
        //Get the X angle of the Canon
        if(_head.transform.eulerAngles.x <= 180f)
        {
            _headAngleX = _head.transform.eulerAngles.x;
        }
        else
        {
            _headAngleX = _head.transform.eulerAngles.x - 360f;
        }
        
        //Get the Y angle of the head 
        if(_canon.transform.eulerAngles.y <= 180f)
        {
            _canonAngleY = _canon.transform.eulerAngles.y;
        }
        else
        {
            _canonAngleY = _canon.transform.eulerAngles.y - 360f;
        }
        
        //If the canon's x-axis rotation is out of the limits put it back in
        if (_headAngleX > _headAngleXLowerLimit)
        {
            _headAngleX = _headAngleXLowerLimit;
            _head.transform.localEulerAngles = new Vector3(_headAngleX, 0, 0);
        }
        
        if (_headAngleX < _headAngleXUpperLimit)
        {
            _headAngleX = _headAngleXUpperLimit;
            _head.transform.localEulerAngles = new Vector3(_headAngleX, 0, 0);
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
        _headAngleInput = value.Get<float>();
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