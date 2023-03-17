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

    [SerializeField] public int WalkingSound;

    [SerializeField] private int MaxShoots = 999999;


    private float mouseHorizontal = 1f;
    private float mouseVertical = 1f;

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

        Vector3 MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Debug.Log(MoveDirection);
        MoveDirection = transform.TransformDirection(MoveDirection);

        //pa andar quaiet
        if (Input.GetButton("Fire3"))
        {
            WalkingSound = 3;
            MoveDirection *= silenceSpeed;
            characterController.Move(MoveDirection * Time.deltaTime);
        }
        else
        {
            WalkingSound = 5;
            MoveDirection *= normalMoveSpeed;
            characterController.Move(MoveDirection * Time.deltaTime);
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
        if (other.gameObject.tag == "EnemyAttackTag")
        {
            //Debug.Log("Me atacan");
            transform.SetPositionAndRotation(SpawnPoint.gameObject.transform.position, InitialRotation);
        }
    }
}
