using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : PlayerControl
{
    bool canDeflect = true;
    bool canDash = true;
    bool canShuriken = true;
    bool canTeleport = true;
    public Transform throwingPoint;
    public GameObject canDashColide;
    public GameObject Shuriken;
    public GameObject teleportBall;
    public GameObject teleportEffect;
    public GameObject deflector;
    public float dashPower;
    public float dashTime;
    public float teleportPower;
    public GameObject Avatar;
    public Material normal, transparent;
    public GameObject trailEffect;
    public Animator hurtPanel;
    [Header("UI")]
    public Image dashImage;
    public Sprite[] dashSprites;
    public Image deflectImage;
    public Sprite[] deflectSprites;
    public Image shurikenImage;
    public Sprite[] shurikenSprites;
    public Image teleportImage;
    public Sprite[] teleportSprites;
    AudioSource deflectSound;

    public override void Start()
    {
        base.Start();
        canDashColide.SetActive(false);
        Avatar.GetComponent<Renderer>().material = normal;
        trailEffect.SetActive(false);
        deflector.SetActive(false);
        deflectSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Deflecting
        if (Input.GetMouseButtonDown(0) && isLanding == true && canDeflect == true)
        {
            canDeflect = false;
            deflectImage.sprite = deflectSprites[1];
            StealthOff();
            trailEffect.SetActive(true);
            deflector.SetActive(true);
            deflectSound.Play();
            anim.SetTrigger("Attack");
            StartCoroutine(DeflectCoolDown());
        }
        //Dashing
        if (Input.GetKeyDown(KeyCode.E) && canDash == true)
        {
            canDash = false;
            dashImage.sprite = dashSprites[1];
            StealthOff();
            anim.SetTrigger("Dash");
            StartCoroutine(DashCoolDown(dashTime));
        }
        //Shuriken
        if (Input.GetKeyDown(KeyCode.R) && canShuriken == true)
        {
            canShuriken = false;
            shurikenImage.sprite = shurikenSprites[1];
            StealthOff();
            anim.SetTrigger("Throw");
            StartCoroutine(ShurikenCoolDown());
        }
        //Teleporting
        if (Input.GetMouseButtonDown(1) && canTeleport == true)
        {
            canTeleport = false;
            teleportImage.sprite = teleportSprites[1];
            anim.SetTrigger("Teleport");
            StartCoroutine(TeleportCoolDown());
        }
        //StealthMode
        if (Input.GetKeyDown(KeyCode.C))
        {
            StealthOn();
        }

    }

    IEnumerator DeflectCoolDown()
    {
        yield return new WaitForSeconds(2.2f);
        trailEffect.SetActive(false);
        deflector.SetActive(false);
        deflectImage.sprite = deflectSprites[0];
        canDeflect = true;
    }
    IEnumerator DashCoolDown(float dashTime)
    {
        rb.velocity = Camera.main.transform.forward * dashPower;
        //The time of dash
        canDashColide.SetActive(true);
        yield return new WaitForSeconds(dashTime);
        canDashColide.SetActive(false);
        rb.velocity = Vector3.zero;
        //Cooldown
        yield return new WaitForSeconds(2.2f);
        CanDash();
    }
    IEnumerator ShurikenCoolDown()
    {
        yield return new WaitForSeconds(1.2f);
        shurikenImage.sprite = shurikenSprites[0];
        canShuriken = true;
    }
    IEnumerator TeleportCoolDown()
    {
        yield return new WaitForSeconds(2.0f);
        teleportImage.sprite = teleportSprites[0];
        canTeleport = true;
    }

    public void ThrowEvent()
    {
        Instantiate(Shuriken, throwingPoint.position, transform.rotation);
        Instantiate(Shuriken, throwingPoint.position + new Vector3(0, -0.07f, 0.15f), transform.rotation);
        Instantiate(Shuriken, throwingPoint.position + new Vector3(0, 0.07f, 0.3f), transform.rotation);
    }
    public void Teleport()
    {
        GameObject ball = Instantiate(teleportBall, throwingPoint.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * teleportPower, ForceMode.Impulse);
        Destroy(ball, 1.4f);
        StartCoroutine(Translocating(ball));
    }
    //Translocating
    IEnumerator Translocating(GameObject ball)
    {
        yield return new WaitForSeconds(1.35f);
        transform.position = ball.transform.position;
        Instantiate(teleportEffect, transform.position, Quaternion.identity);

    }
    //Bonus Dash
    public void CanDash()
    {
        canDash = true;
        dashImage.sprite = dashSprites[0];
    }

    void StealthOn()
    {
        GetComponent<PlayerControl>().stealthMode = true;
        Avatar.GetComponent<Renderer>().material = transparent;
    }
    void StealthOff()
    {
        GetComponent<PlayerControl>().stealthMode = false;
        Avatar.GetComponent<Renderer>().material = normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StealthOff();
            hurtPanel.SetTrigger("Hurt");
        }
    }
}
