using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public GameManager gamemanager;
    public Reload reload;
    private PasswordCanvasManager passwordCanvasManager;
    private TrapDoor actualTrapDoor;
    private NormalDoor actualNormalDoor;
    private HackingController hackingController;

    private bullet Bullet;
    public bullet sound_bullet;
    public Camera Cam;
    public Transform boquilla;

    //public GameObject SpawnPoint;

    CharacterController characterController;

    [SerializeField] private float normalMoveSpeed = 5f;

    private float currentSpeed;

    private float Gravity = -9.8f;

    [SerializeField] public int WalkingSound;

    private int maxShoots;
    //variable a definir que marca el maximo de balas que pueden estar activas al mismo tiempo
    [SerializeField] private int constantMaxShoots;

    private Quaternion InitialRotation;
    private Vector3 InitialPos;

    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;

    public bool IsWalking = false;
    public bool Shooting = false;
    public bool IsHacking = false;

    public bool CanMove;

    [SerializeField] private GameObject bulletScrollbar;

    public AudioSource pasos;
    public AudioSource disparo;

    private bool Hactive;
    private bool Vactive;
    private bool pulsadaR;

    private bool nullHackingController;

    private void Awake()
    {
        CanMove = true;

        disparo = GetComponent<AudioSource>();

        characterController = GetComponent<CharacterController>();

        if (FindObjectOfType<PasswordCanvasManager>() != null)
            passwordCanvasManager = FindObjectOfType<PasswordCanvasManager>();

        if (FindObjectOfType<HackingController>() != null)
        {
            nullHackingController = false;
            hackingController = FindObjectOfType<HackingController>();
        }
        else
            nullHackingController = true;


        maxShoots = constantMaxShoots;
        InitialRotation = transform.rotation;
        InitialPos = transform.position;
    }

    private void Start()
    {
        bulletScrollbar.SetActive(true);
    }

    void Update()
    {
        Bullet = FindObjectOfType<bullet>();

        if (Bullet == null)
            maxShoots = constantMaxShoots;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (actualTrapDoor != null)
            {
                actualTrapDoor.isOpened = !actualTrapDoor.isOpened;
            }            
            
            if (actualNormalDoor != null)
            {
                actualNormalDoor.isOpened = !actualNormalDoor.isOpened;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            pulsadaR = true;
        else
            pulsadaR = false;


        if (!GameManager.Instance().IsPaused() && (passwordCanvasManager == null || !passwordCanvasManager.colliderMinijuegoNumeros.doingGame))
        {
            if (CanMove)
            {
                if (pulsadaR)
                {
                    StartCoroutine(reload.ReloadWeapon());
                }
                Move();
                Shoot();
            }
            MouseMove();    
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Hackeo
        //Hacking(light5, scrollbar5, interactText);

        //Debug.Log(colliderMinijuegoNumeros.gameObject.name.ToString());
    }

    private void MouseMove() {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);
        Cam.transform.Rotate(v_mouse, 0, 0);

        v_mouse = Mathf.Clamp(v_mouse, -80, 80);
    }

    void Move()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            Hactive = true;
            pasos.Play();
        }

        if (Input.GetButtonDown("Vertical"))
        {
            Vactive = true;
            pasos.Play();
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            Hactive = false;

            if (Vactive == false)
            {
                pasos.Pause();
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            Vactive = false;

            if (Hactive == false)
            {
                pasos.Pause();
            }
        }

        Vector3 MoveDirection = new Vector3(Input.GetAxis("Horizontal"), Gravity, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);
        if (Math.Abs(MoveDirection.x) >0.1f || Math.Abs(MoveDirection.z)>0.1f)
        {
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }
        WalkingSound = 5;
        currentSpeed = normalMoveSpeed;
        characterController.Move(MoveDirection * currentSpeed * Time.deltaTime);
    }

    //disparo
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (reload.RemainingAmmo > 0 && maxShoots > 0)
            {
                Shooting = true;
                disparo.Play();
                bullet bullet = Instantiate(sound_bullet, boquilla.position, Quaternion.Euler(boquilla.forward));
                reload.RemainingAmmo--;
                maxShoots--;
                reload.bulletFillAmount -= 0.1f;
                reload.bulletProgressBar.fillAmount = reload.bulletFillAmount;
                Shooting = false;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            characterController.enabled = false;
            transform.SetPositionAndRotation(InitialPos, InitialRotation);
            IsHacking = false;
            if (!nullHackingController)
                hackingController.DeactivateHackingUI();
            characterController.enabled = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("TrapDoor"))
        {
            actualTrapDoor = null;
        }
        if (other.gameObject.CompareTag("NormalDoor"))
        {
            actualNormalDoor = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finalLevel"))
        {
            SceneManager.LoadScene("lvl3");
        }
        if (other.gameObject.CompareTag("TrapDoor"))
        {
            actualTrapDoor = other.gameObject.GetComponentInParent<TrapDoor>();
        }

        if (other.gameObject.CompareTag("NormalDoor"))
        {
            actualNormalDoor = other.gameObject.GetComponentInParent<NormalDoor>();
        }
        if (other.gameObject.GetComponent<Spawnpoint>())
        {
            InitialRotation = transform.rotation;
            InitialPos = other.gameObject.GetComponent<Spawnpoint>().transform.position;
        }

    }

}
