using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public GameManager GameManager;

    public Camera Cam;
    public bullet sound_bullet;
    public Transform boquilla;

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

    private Quaternion InitialRotation;
    private Vector3 InitialPos;

    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    float h_mouse;
    float v_mouse;

    //Abrir puertas

    public bool IsHacking = false;

    static public List<LightObject> lights;


    public bool CanOpen1;
    public bool CanOpen2;
    //Hackeo

    private bool CanMove;

    [SerializeField] private Text interactText; //texto interactuar con E
    [SerializeField] private Text hackingText; //Texto hackeo
    [SerializeField] private Image progressBar;

    [SerializeField] private bool isInteractable;
    [SerializeField] private float fillAmount; //progreso del hackeo

    [SerializeField] private GameObject scrollbar;

    private void Awake()
    {
        lights = new(FindObjectsOfType<LightObject>());
        lights.Sort((a, b) => { return a.name.CompareTo(b.name); });

        for (int i = 0; i < lights.Count; i++)
        {
            Debug.Log(lights[i]);
        }


        InitialRotation = transform.rotation;
        InitialPos = transform.position;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        hackingText.enabled = false;
        interactText.enabled = false;
        scrollbar.SetActive(false);
        progressBar.fillAmount = 0f;
        isInteractable = false;
        fillAmount = 0f;
    }

    void Update()
    {
        //Debug.Log(fillAmount);
        //Debug.Log(InitialPos);
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

        if (lights[0].IsActivated == true && lights[1].IsActivated == true) { CanOpen1 = true; }
        if (lights[2].IsActivated == true && lights[3].IsActivated == true && lights[4].IsActivated == true) { CanOpen2 = true; }

        //Hackeo
        //Hacking(light5, scrollbar5, interactText);

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
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RemainingAmmo > 0)
            {
                bullet bullet = Instantiate(sound_bullet, boquilla.position, Quaternion.Euler(boquilla.forward));
                RemainingAmmo--;
            }
        }
        StartCoroutine(CShootInterval());
    }
    IEnumerator CShootInterval()
    {
        yield return new WaitForSeconds(ShootInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttackTag"))
        {
            Debug.Log("Ataque");
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
            Hacking(lights[0]);
        }
        if (other.gameObject.CompareTag("Object2") && !lights[1].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[1]);
        }
        if (other.gameObject.CompareTag("Object3") && !lights[2].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[2]);
        }
        if (other.gameObject.CompareTag("Object4") && !lights[3].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[3]);
        }
        if (other.gameObject.CompareTag("Object5") && !lights[4].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[4]);
        }
        if (other.gameObject.CompareTag("FinalPC"))
        {
            ActivateHackingUI();
            FinalHacking();
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

    private void Hacking(LightObject light)
    {
        if (isInteractable)
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
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    light.ActivateLight(true);
                    DeactivateHackingUI();
                }
            }
            else
            {
                IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }
    private void FinalHacking()
    {
        ActivateHackingUI();
        if (isInteractable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fillAmount = 0f;
            }

            if (Input.GetKey(KeyCode.E))
            {
                IsHacking = true;
                HackingBar();
                fillAmount += (Time.deltaTime / 10);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    DeactivateHackingUI();
                    GameManager.ChangeScene(0);
                }
            }
            else
            {
                IsHacking = false;
                DeactivateHackingBar();
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
    //private void CheckLightBool(bool checklight) {
    //    if (checklight == light1Activated) { Debug.Log("1"); }
    //    else if (checklight == light2Activated) { Debug.Log("2"); }
    //    else if (checklight == light3Activated) { Debug.Log("3"); }
    //    else if (checklight == light4Activated) { Debug.Log("4"); }
    //    else if (checklight == light5Activated) { Debug.Log("5"); }
    //}

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemainingAmmo = MaxAmmo;
        }
    }
}
