using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float moveSpeed = 10.0f;
    private float camSpeed = 50.0f;
    private Rigidbody rb;
    private BoxCollider hitbox;
    public GameObject Cam;
    private Rigidbody cam;
    private Quaternion camOrigin;
    private Vector3 origin;
    private float rotationY;
    
    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        hitbox = GetComponent<BoxCollider>();
        cam = Cam.GetComponent<Rigidbody>();
        cam.freezeRotation = true;
        origin = rb.position;
        camOrigin = cam.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float camHor = Input.GetAxis("Mouse X");
        float camVert = Input.GetAxis("Mouse Y");
        HandleMovement(horizontal, vertical);
        HandleCamera(camHor, camVert);
        
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1")) // Wall Clip
        {
            hitbox.enabled = !hitbox.enabled;
        }
        
        if (Input.GetButtonDown("Fire2")) //Reset Position
        {
            rb.position = origin;
            rb.angularVelocity = new Vector3(0, 0, 0);
            rb.velocity = new Vector3(0, 0, 0);
            cam.angularVelocity = new Vector3(0, 0, 0); //fix camera
            cam.transform.rotation = camOrigin; 

        }

        //ToggleFog
        //ToggleDay

    }

    void HandleMovement(float horizontal, float vertical) //Movement Subsystem
    {
        rb.AddForce(rb.transform.forward * moveSpeed * vertical); //forward and backwards motion
        rb.AddForce(rb.transform.right * moveSpeed * horizontal); //left and right motion
    }

    void HandleCamera(float horizontal, float vertical) //camera subsystem
    {
        //side-to-side rotation
        rb.transform.Rotate(0, horizontal * camSpeed, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);


        rotationY = rb.transform.localEulerAngles.y;
        //up down rotation
        cam.transform.Rotate(vertical * camSpeed, 0, 0);
        cam.angularVelocity = new Vector3(0, 0, 0);
        if (cam.transform.localEulerAngles.x  < 315.0f && cam.transform.localEulerAngles.x > 180)
        {
            cam.transform.rotation = Quaternion.Euler(315.0f, rotationY, 0);
        } else if (cam.transform.localEulerAngles.x > 45.0f && cam.transform.localEulerAngles.x < 180) {
            cam.transform.rotation = Quaternion.Euler(45.0f, rotationY, 0);
        }
        
    }

}
