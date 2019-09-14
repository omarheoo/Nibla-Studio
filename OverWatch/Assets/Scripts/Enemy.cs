using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bullet;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.LookAt(player);
    }

    public IEnumerator Shoot()
    {
        Instantiate(bullet, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(3.0f);
    }
   
}
