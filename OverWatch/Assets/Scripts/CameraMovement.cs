using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed;
    public Transform playerHips, player;
    float mouseX, mouseY;
    Animator anim;
    bool thirdPerson = true;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY,-30, 70);
        playerHips.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        if (thirdPerson)
        {
            transform.LookAt(playerHips);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            thirdPerson = !thirdPerson;
            if(thirdPerson == true)
            {
                anim.SetBool("ThirdPerson", true);
                anim.SetBool("FirstPerson", false);
            }
            else
            {
                anim.SetBool("ThirdPerson", false);
                anim.SetBool("FirstPerson", true);
            }
        }
    }
}
