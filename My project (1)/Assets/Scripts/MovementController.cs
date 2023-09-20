using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public float rotationSpeed = 2.0f;
    public Animator anim;

    void Start()
    {
        
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("IsWalking", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("IsWalking", false);
        }
    }
}
