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
    private ColliderMinijuegoNumeros colliderMinijuegoNumeros;
    private PasswordCanvasManager passwordCanvasManager;
    private TrapDoor actualTrapDoor;
    private NormalDoor actualNormalDoor;
    public Reload reload;

    private bullet Bullet;
    public bullet sound_bullet;
    public Camera Cam;
    public Transform boquilla;

    public int CurrentLevel;

    [Header ("Audio")]
    public AudioSource audioWalk;

    //public GameObject SpawnPoint;

    CharacterController characterController;

    [SerializeField] private float normalMoveSpeed = 5f;

    private float currentSpeed;

    private float Gravity = -9.8f;

    [SerializeField] public int WalkingSound;

    private int maxShoots;
    [SerializeField] private int constantMaxShoots;
    private bool CanReload;

    private Quaternion InitialRotation;
    private Vector3 InitialPos;

    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;

    //Abrir puertas

    public bool IsHacking = false;
    public bool IsWalking = false;
    public bool Shooting = false;

    public List<LightObject> lights;
    public List<PcLightObject> PClights;


    public bool CanOpen1;
    public bool CanOpen2;
    //Hackeo

    private bool CanMove;

    [SerializeField] private Text interactText; //texto interactuar con E
    [SerializeField] private Text hackingText; //Texto hackeo
    [SerializeField] private bool isInteractable;

    private float fillAmount; //progreso del hackeo

    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject scrollbar;

    [SerializeField] private GameObject bulletScrollbar;
    


    //Audio

    public AudioSource pasos;

    private bool Hactive;
    private bool Vactive;
    private bool pulsandoE;
    private bool pulsadaE;
    private bool pulsadaR;

    private void Awake()
    {
        audioWalk = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();


        if (FindObjectOfType<ColliderMinijuegoNumeros>() != null)
            colliderMinijuegoNumeros = FindObjectOfType<ColliderMinijuegoNumeros>();

        lights = new(FindObjectsOfType<LightObject>());
        lights.Sort((a, b) => { return a.name.CompareTo(b.name); });


        if (FindObjectOfType<PasswordCanvasManager>() != null)
            passwordCanvasManager = FindObjectOfType<PasswordCanvasManager>();

        PClights = new(FindObjectsOfType<PcLightObject>());
        PClights.Sort((a, b) => { return a.name.CompareTo(b.name); });


        maxShoots = constantMaxShoots;
        InitialRotation = transform.rotation;
        InitialPos = transform.position;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
    }

    void Start()
    {
        hackingText.enabled = false;
        interactText.enabled = false;
        isInteractable = false;

        scrollbar.SetActive(false);
        bulletScrollbar.SetActive(true);

        progressBar.fillAmount = 0f;
        fillAmount = 0f;
    }

    void Update()
    {
        Bullet = FindObjectOfType<bullet>();

        if (Bullet == null)
            maxShoots = constantMaxShoots;

        if (Input.GetKeyDown(KeyCode.E))
        {
            fillAmount = 0f;
            if (actualTrapDoor != null)
            {
                actualTrapDoor.isOpened = !actualTrapDoor.isOpened;
            }            
            
            if (actualNormalDoor != null)
            {
                actualNormalDoor.isOpened = !actualNormalDoor.isOpened;
            }
        }

        //Assignar Inputs
        //E
        if (Input.GetKeyDown(KeyCode.E))
            pulsadaE = true;
        else
            pulsadaE = false;

        if (Input.GetKey(KeyCode.E))
            pulsandoE = true;
        else
            pulsandoE = false;
        //R
        if (Input.GetKeyDown(KeyCode.R))
            pulsadaR = true;
        else
            pulsadaR = false;



        if (Input.GetKey(KeyCode.H)) 
        {
            gamemanager.GODmode();
            for (int i = 0; i < lights.ToArray().Length; i++)
            {
                gamemanager.GODmodeActivateLights(lights[i], PClights[i]);
            }
        }

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

        if (!IsHacking) { CanMove = true; }
        else { CanMove = false; }

        

        if (CurrentLevel == 1)
        {
            if (lights[0].IsActivated == true && lights[1].IsActivated == true) { 
                CanOpen1 = true;
            }
            if (lights[2].IsActivated == true && lights[3].IsActivated == true && lights[4].IsActivated == true) { CanOpen2 = true; }
        }
        if (CurrentLevel == 2)
        {
            //trapdoormanager.ActivateDoor();
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
            DeactivateHackingUI();
            characterController.enabled = true;
            //Debug.Log(transform.position);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Object1") && !lights[0].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[0], PClights[0]);
        }
        if (other.gameObject.CompareTag("Object2") && !lights[1].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[1], PClights[1]);
        }
        if (other.gameObject.CompareTag("Object3") && !lights[2].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[2], PClights[2]);
        }
        if (other.gameObject.CompareTag("Object4") && !lights[3].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[3], PClights[3]);
        }
        if (other.gameObject.CompareTag("Object5") && !lights[4].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[4], PClights[4]);
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object1") ||
            other.CompareTag("Object2") ||
            other.CompareTag("Object3") ||
            other.CompareTag("Object4") ||
            other.CompareTag("Object5") ||
            other.CompareTag("FinalPC")) { DeactivateHackingUI(); }
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
            SceneManager.LoadScene(0);
        }
        if (other.gameObject.CompareTag("TrapDoor"))
        {
            actualTrapDoor = other.gameObject.GetComponentInParent<TrapDoor>();
        } 
        
        if (other.gameObject.CompareTag("NormalDoor"))
        {
            actualNormalDoor = other.gameObject.GetComponentInParent<NormalDoor>();
        }
    }

    private void Hacking(LightObject light, PcLightObject PcLight)
    {
        if (isInteractable)
        {   
            if (pulsandoE)
            {
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                IsHacking = true;
                if (fillAmount >= 1f)
                {
                    IsHacking = false;
                    light.ActivateLight(true);
                    PcLight.GetComponent<Light>().color = Color.green;
                    DeactivateHackingUI();
                }
            }
            else
            {
                scrollbar.SetActive(false);
                IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }

    private void HackingBar()
    {
        scrollbar.SetActive(true);
        hackingText.enabled = true;
    }
    private void DeactivateHackingBar()
    {
        scrollbar.SetActive(false);
        hackingText.enabled = false;
    }
    private void ActivateHackingUI()
    {
        isInteractable = true;
        progressBar.enabled = true;
        interactText.enabled = true;
    }
    private void DeactivateHackingUI()
    {
        isInteractable = false;
        interactText.enabled = false;
        hackingText.enabled = false;
        progressBar.enabled = false;
        progressBar.fillAmount = 0f;
        fillAmount = 0f;
        scrollbar.SetActive(false);
    }
}
