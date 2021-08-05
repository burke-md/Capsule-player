using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalImput;
    private Rigidbody rigidbodyComponent;
    
    // Start is called before the first frame update
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

        horizontalImput = Input.GetAxis("Horizontal");
    }
    //FixedUpdate is called onceevery physics update (100/s)
    private void FixedUpdate() 
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

        rigidbodyComponent.velocity = new Vector3(horizontalImput, rigidbodyComponent.velocity.y, 0);

    }


}

