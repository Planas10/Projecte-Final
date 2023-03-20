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


    [SerializeField] private float silenceSpeed = 2f;

    [SerializeField] private float normalMoveSpeed = 5f;

    private float currentSpeed;
    
    private float Gravity = -9.8f;

    [SerializeField] public int WalkingSound;

    [SerializeField] private int MaxShoots = 999999;


    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;


    private void Awake()
    {

        transform.position = SpawnPoint.transform.position;

        //Guardar rotación inicial
        InitialRotation = transform.rotation;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
        characterController = GetComponent<CharacterController>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (!GameManager.Instance().IsPaused())
        {
            Move();
            Shoot();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Move()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);
        Cam.transform.Rotate(v_mouse, 0, 0);

        v_mouse = Mathf.Clamp(v_mouse, -80, 80);

        Vector3 MoveDirection = new Vector3(Input.GetAxis("Horizontal"), Gravity, Input.GetAxis("Vertical"));
        //Debug.Log(MoveDirection);
        MoveDirection = transform.TransformDirection(MoveDirection);

        //Debug.Log(MoveDirection);

        //pa andar quaiet
        if (Input.GetButton("Fire3"))
        {
            WalkingSound = 3;
            currentSpeed = silenceSpeed;
            characterController.Move(MoveDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            WalkingSound = 5;
            currentSpeed = normalMoveSpeed;
            characterController.Move(MoveDirection * currentSpeed * Time.deltaTime);
        }
        //Debug.Log(WalkingSound);
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
        if (other.gameObject.CompareTag("EnemyAttackTag"))
        {
            Debug.Log("Me atacan");
            transform.SetPositionAndRotation(SpawnPoint.transform.position, InitialRotation);
        }
    }
}
