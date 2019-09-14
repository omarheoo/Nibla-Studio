using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed;
    public GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Shoot();
        Destroy(gameObject, 3.0f);
    }

    public void Shoot()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
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
