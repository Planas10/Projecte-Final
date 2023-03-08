using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera Cam;
    public bullet sound_bullet;
    public Transform boquilla;

    //velocidad de movimiento de la camara
    private float mouseHorizontal = 1f;
    private float mouseVertical = 1f;

    float h_mouse;
    float v_mouse;

    private float speed;

    [Header("Stats")]
    public float moveSpeed = 2.5f;
    public float silenceSpeed = 0.3f;
    public float shootInterval;
    private float ShootIntensity = 10f;
    private int MaxShoots = 999999;
    public int CurrentShoots = 0;

    public Rigidbody rb;


    float h;
    float v;

    CharacterController characterController;

    // Start is called before the first frame update
    private void Awake()
    {
        

        characterController = GetComponent<CharacterController>();
        //esconder el mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentShoots);
        Move();
        Shoot();
    }

    void Move()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);
        Cam.transform.Rotate(v_mouse, 0, 0);

        v_mouse = Mathf.Clamp(v_mouse, -80, 80);

        //movimiento del player

        speed = moveSpeed;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //pa andar quaiet
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

    //disparo
    private void Shoot() {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentShoots < MaxShoots)
            {
                bullet bullet = Instantiate(sound_bullet, boquilla.position, Quaternion.Euler(boquilla.forward));
                CurrentShoots++;
            }
        }      
    }
}
