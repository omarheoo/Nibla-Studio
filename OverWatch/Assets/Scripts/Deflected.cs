using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflected : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed;
    Vector3 mForward;
    public GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5.5f);
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
