using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float bulletForce = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get RigidBody
        GetComponent<Rigidbody>().linearVelocity = transform.forward * bulletForce;
        //Destroy
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != gameObject)
        {
            Debug.Log("Collision Enter ??????? : " + other.gameObject.name);
        
            if (other.gameObject.TryGetComponent(out Destructible objective))
            {
                objective.TakeDamage(10);
                Destroy(gameObject);
            }
        }

        
    }
    
    // private void OnCollisionStay(Collision other)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // private void OnCollisionExit(Collision other)
    // {
    //     throw new NotImplementedException();
    // }

    // Update is called once per frame
    void Update()
    {
    }
}
