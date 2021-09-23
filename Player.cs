using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] Transform playerCamera = null;

    [SerializeField] float mouseSensitivity = 4.25f;

    private bool jumpKeyWasPressed;
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

        xInput = Input.GetAxis("Horizontal");

        zInput = Input.GetAxis("Vertical");
        
        UpdateMouseLook();
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
    void UpdateMouseLook()
    {
        //Capture direction from mouse
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
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

