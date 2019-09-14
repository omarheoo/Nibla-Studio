using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed;
    Vector3 mForward;
    public GameObject deflected;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5.5f);
        mForward = transform.forward;
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        rb.velocity = transform.forward * bulletSpeed;
        //rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Deflector"))
        {
            GameObject dBullet = Instantiate(deflected, transform.position, Quaternion.identity);
            dBullet.transform.forward = -mForward;
            Destroy(gameObject);
        }
    }
   
}
