using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public Camera Cam;
    Rigidbody rb;

    public float mouseHorizontal = 3f;
    public float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;

    public float moveSpeed = 2.5f;
    public float silenceSpeed = 0.3f;

    float h;
    float v;


    bool floorDetected = false;
    bool isJump = false;
    public float jumpForce = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //esconder el mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {

        //movimiento de camara
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);
        Cam.transform.Rotate(v_mouse, 0, 0);

        //movimiento del player
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //pa correr
        if (Input.GetButton("Fire3"))
            transform.Translate(direction * silenceSpeed * Time.deltaTime);
        else
            transform.Translate(direction * moveSpeed * Time.deltaTime);

        Vector3 floor = transform.TransformDirection(Vector3.down);

        //solo 1 salto
        //if (Physics.Raycast(transform.position, floor, 1.03f))
        //{
        //    floorDetected = true;
        //    //print("suelo");
        //}
        //else
        //{
        //    floorDetected = false;
        //    //print("no suelo");
        //}
        //isJump = Input.GetButtonDown("Jump");

        //if (isJump && floorDetected)
        //{
        //    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        //}
    }
}
