using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    GameObject player;
    public GameObject slashEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Just to keep it's velocity after coliding
            player.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * player.GetComponent<Abilities>().dashPower;
            //Effect
            GameObject effect = Instantiate(slashEffect, transform.position+new Vector3(0,1.0f,0), Quaternion.identity);
            Destroy(effect.gameObject, 0.2f);
            Destroy(other.gameObject);
            player.GetComponent<Abilities>().CanDash();
        }
    }
}
