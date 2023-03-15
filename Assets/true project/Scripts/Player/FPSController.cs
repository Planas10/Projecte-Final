using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera Cam;
    public bullet sound_bullet;
    public Transform boquilla;
    public Rigidbody rb;
    public GameObject SpawnPoint;

    CharacterController characterController;
    SphereCollider sphereCollider;

    private Quaternion InitialRotation;

    //Movimiento Player
    [Header("Stats")]
    public float silenceSpeed = 0.3f;
    public float moveSpeed = 2.5f;
    private float speed;
    public int WalkingSound;

    float h;
    float v;

    //Disparo
    private int MaxShoots = 999999;

    //Movimiento camara
    private float mouseHorizontal = 1f;
    private float mouseVertical = 1f;

    float h_mouse;
    float v_mouse;


    private void Awake()
    {

        transform.position = SpawnPoint.transform.position;

        //esconder el mouse
        Cursor.lockState = CursorLockMode.Locked;

        //Guardar rotación inicial
        InitialRotation = transform.rotation;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
        characterController = GetComponent<CharacterController>();
        sphereCollider = GetComponent<SphereCollider>();
    }

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
            WalkingSound = 3;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            WalkingSound = 5;
            speed = moveSpeed;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        //Debug.Log(WalkingSound);

        Vector3 floor = transform.TransformDirection(Vector3.down);
    }

    //disparo
    private void Shoot() {
        if (Input.GetMouseButtonDown(0))
        {
            if (MaxShoots > 0)
            {
                bullet bullet = Instantiate(sound_bullet, boquilla.position, Quaternion.Euler(boquilla.forward));
            }
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttackTag")
        {
            //Debug.Log("Me atacan");
            transform.SetPositionAndRotation(SpawnPoint.gameObject.transform.position, InitialRotation);
        }
    }
}
