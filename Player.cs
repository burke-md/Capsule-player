using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    // private float horizontalInput;
    // private float verticalInput;
    private float xInput;
    private float zInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining = 0;

    
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
         jumpKeyWasPressed = true;
        }

        // horizontalInput = Input.GetAxis("Horizontal");
        xInput = Input.GetAxis("Horizontal");

        // verticalInput = Input.GetAxis("Vertical");
        zInput = Input.GetAxis("Vertical");
    }

    //FixedUpdate is called onceevery physics update (100/s)
    private void FixedUpdate() 
    {

        rigidbodyComponent.velocity = new Vector3(xInput, rigidbodyComponent.velocity.y, zInput);


        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 1.5f;
                superJumpsRemaining--;
            }

            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }



    }
    private void OnTriggerEnter(Collider other)
    {
        //Collect coins logic
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;

        }
    }

}

