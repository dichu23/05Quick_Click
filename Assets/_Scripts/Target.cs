using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float minforce = 12,
        maxforce = 16,
        maxtorque = 10, 
        xRange = 4,
        ySpawpos = -6;

    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse) ;
        transform.position = RandomSpawnPosition();  //z=0

    }

    private Vector3 RandomForce()
    {

        return Vector3.up * Random.Range(minforce, maxforce);

    }
    private float RandomTorque()
    {
        return Random.Range(-maxtorque, maxtorque);

    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawpos);
    }

   
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone"))
        {
            Destroy(gameObject);
        }
    }
}
