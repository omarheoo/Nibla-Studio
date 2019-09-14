using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    protected bool isLanding;
    protected float speed;
    public bool stealthMode;
    public float walkSpeed;
    public float runSpeed;
    public float stealthSpeed;
    public float jumpHeight;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;

    public virtual void Start()
    {
        isLanding = true;
        stealthMode = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }   

    void Update()
    {
        // W A S D
        var z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(x, 0, z);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isLanding)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            isLanding = false;
        }
        //Stealthing
        if (stealthMode)
        {
            speed = stealthSpeed;
        }
        /////////////Animation
        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isLanding)
            {
                if (stealthMode)
                {
                    speed = runSpeed + stealthSpeed;
                }
                else
                {
                    speed = runSpeed;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", true);//
                    anim.SetBool("Idle", false);
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", false);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", true);//
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", false);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("RightRun", true);//
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", false);
                }
                else
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", true);//
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", false);
                }
            }
        }
        //Walking
        else
        {
            if (isLanding)
            {
                if (stealthMode)
                {
                    speed = walkSpeed +stealthSpeed;
                }
                else
                {
                    speed = walkSpeed;
                }
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("Walk", true);//
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", false);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", true);//
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", true);//
                    anim.SetBool("LeftWalk", false);
                }
                else
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("RightRun", false);
                    anim.SetBool("LeftRun", false);
                    anim.SetBool("RightWalk", false);
                    anim.SetBool("LeftWalk", false);
                    anim.SetBool("Idle", true);//
                }
            }
        }
        //Restart Scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(Landing());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isLanding = false;
        }
    }

    IEnumerator Landing()
    {
        yield return new WaitForSeconds(.35f);
        isLanding = true;
    }
}



