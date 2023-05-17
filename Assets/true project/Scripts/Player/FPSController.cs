using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public GameManager gamemanager;

    public Camera Cam;
    public bullet sound_bullet;
    public Transform boquilla;

    public int CurrentLevel;

    //public GameObject SpawnPoint;

    CharacterController characterController;

    [SerializeField] private float silenceSpeed = 2f;

    [SerializeField] private float normalMoveSpeed = 5f;

    private float currentSpeed;

    private float Gravity = -9.8f;

    [SerializeField] public int WalkingSound;

    private float ShootInterval = 10f;
    public int MaxAmmo;
    public int RemainingAmmo;
    private bool CanReload;

    private Quaternion InitialRotation;
    private Vector3 InitialPos;

    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;

    //Abrir puertas

    public bool IsHacking = false;

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

    private float bulletFillAmount = 1f;
    private float reloadTime = 3f;
    private float reloadingTime;
    private bool reloading = false;
    [SerializeField] private Image bulletProgressBar;
    [SerializeField] private GameObject bulletScrollbar;

    //Audio

    public AudioSource pasos;

    private bool Hactive;
    private bool Vactive;



    private void Awake()
    {
        lights = new(FindObjectsOfType<LightObject>());
        lights.Sort((a, b) => { return a.name.CompareTo(b.name); });

        PClights = new(FindObjectsOfType<PcLightObject>());
        PClights.Sort((a, b) => { return a.name.CompareTo(b.name); });

        InitialRotation = transform.rotation;
        InitialPos = transform.position;

        reloadingTime = reloadTime;
        bulletProgressBar.fillAmount = bulletFillAmount;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        RemainingAmmo = MaxAmmo;
        hackingText.enabled = false;
        interactText.enabled = false;
        scrollbar.SetActive(false);
        bulletScrollbar.SetActive(true);
        progressBar.fillAmount = 0f;
        isInteractable = false;
        fillAmount = 0f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H)) 
        {
            gamemanager.GODmode();
            for (int i = 0; i < lights.ToArray().Length; i++)
            {
                gamemanager.GODmodeActivateLights(lights[i], PClights[i]);
            }
        }
        if (!GameManager.Instance().IsPaused())
        {
            if (CanMove)
            {
                Reload();
                Move();
                Shoot();
            }
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (!IsHacking) { CanMove = true; }
        else { CanMove = false; }

        //Debug.Log(SpawnPoint.transform.position);

        if (CurrentLevel == 1)
        {
            if (lights[0].IsActivated == true && lights[1].IsActivated == true) { CanOpen1 = true; }
            if (lights[2].IsActivated == true && lights[3].IsActivated == true && lights[4].IsActivated == true) { CanOpen2 = true; }
        }
    }

    void Move()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);
        Cam.transform.Rotate(v_mouse, 0, 0);

        v_mouse = Mathf.Clamp(v_mouse, -80, 80);

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

        WalkingSound = 5;
        currentSpeed = normalMoveSpeed;
        characterController.Move(MoveDirection * currentSpeed * Time.deltaTime);
    }

    //disparo
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RemainingAmmo > 0)
            {
                bullet bullet = Instantiate(sound_bullet, boquilla.position, Quaternion.Euler(boquilla.forward));
                RemainingAmmo--;
                bulletFillAmount -= 0.1f;
                bulletProgressBar.fillAmount = bulletFillAmount;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AI_Enemy>())
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

        if (other.gameObject.CompareTag("TrapDoor"))
        {
            //Debug.Log("PcTrampa1");
            if (Input.GetKeyDown(KeyCode.E))
            {
                //cambiar la puerta de estado
                //conseguir la puerta
                TrapDoor td = other.gameObject.GetComponentInParent<TrapDoor>();
                td.isOpened = !td.isOpened;
            }
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finalLevel"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Hacking(LightObject light, PcLightObject PcLight)
    {
        if (isInteractable )
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fillAmount = 0f;
            }
            if (Input.GetKey(KeyCode.E))
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

    private void Reload()
    {
        if (Input.GetKey(KeyCode.R) && RemainingAmmo < MaxAmmo)
        {
            reloading = true;
            if (reloading == true)
            {
                Debug.Log(reloadingTime);
                reloadingTime -= Time.deltaTime;
            }
            if (reloadingTime <= 0f)
            {
                RemainingAmmo = MaxAmmo;
                reloadingTime = reloadTime;
                bulletFillAmount = 1f;
                bulletProgressBar.fillAmount = bulletFillAmount;
                reloading = false;
            }
        }
    }
}
