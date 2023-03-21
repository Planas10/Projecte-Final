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

    //public GameObject SpawnPoint;

    CharacterController characterController;

    [SerializeField] private float silenceSpeed = 2f;

    [SerializeField] private float normalMoveSpeed = 5f;

    private float currentSpeed;
    
    private float Gravity = -9.8f;

    [SerializeField] public int WalkingSound;

    [SerializeField] public int MaxShoots = 5;
    private float ShootInterval = 10f;
    private int CurrentShoots = 0;

    private Quaternion InitialRotation;
    private Vector3 InitialPos;

    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;

    //Abrir puertas

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject light5;

    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject doorLeft2;
    public GameObject doorRight2;

    private bool light1Activated = false;
    private bool light2Activated = false;
    private bool light3Activated = false;
    private bool light4Activated = false;
    private bool light5Activated = false;

    public GameObject emptyObjectDoorRight;
    public GameObject emptyObjectDoorLeft;
    public GameObject emptyObjectDoorRight2;
    public GameObject emptyObjectDoorLeft2;


    private void Awake()
    {
        InitialRotation = transform.rotation;
        InitialPos = transform.position;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Debug.Log(InitialPos);
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
        //Debug.Log(SpawnPoint.transform.position);

        if (light4Activated == true && light5Activated == true)
        {
            doorRight2.gameObject.SetActive(false);
            doorLeft2.gameObject.SetActive(false);
        }
        if (light1Activated == true && light2Activated == true && light3Activated == true)
        {
            doorRight.gameObject.SetActive(false);
            doorLeft.gameObject.SetActive(false);
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
            if (CurrentShoots < 5)
            {
                bullet bullet = Instantiate(sound_bullet, boquilla.position, Quaternion.Euler(boquilla.forward));
                CurrentShoots++;
            }
        }      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttackTag"))
        {
            //Debug.Log(transform.position);
            transform.SetPositionAndRotation(InitialPos, InitialRotation);
            //Debug.Log(transform.position);
        }

        if (other.gameObject.CompareTag("Object1") && !light1Activated)
        {
            light1.SetActive(true);
            light1Activated = true;
        }
        if (other.gameObject.CompareTag("Object2") && !light2Activated)
        {
            light2.SetActive(true);
            light2Activated = true;
        }
        if (other.gameObject.CompareTag("Object3") && !light3Activated)
        {
            light3.SetActive(true);
            light3Activated = true;
        }
        if (other.gameObject.CompareTag("Object4") && !light4Activated)
        {
            light4.SetActive(true);
            light4Activated = true;
        }
        if (other.gameObject.CompareTag("Object5") && !light5Activated)
        {
            light5.SetActive(true);
            light5Activated = true;
        }
    }
}
