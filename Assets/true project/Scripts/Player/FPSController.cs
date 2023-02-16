using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public Camera Cam;

    public float mouseHorizontal = 2.5f;
    public float mouseVertical = 0.3f;

    float h_mouse;
    float v_mouse;

    private float speed;
    public float moveSpeed = 2.5f;
    public float silenceSpeed = 0.3f;

    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
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

        speed = moveSpeed;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //pa correr
        if (Input.GetButton("Fire3"))
        {
            speed = silenceSpeed;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            speed = moveSpeed;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        Vector3 floor = transform.TransformDirection(Vector3.down);
    }
}
