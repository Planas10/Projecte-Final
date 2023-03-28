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

    public bool IsHacking = false;

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject light5;

    public bool CanOpen1;
    public bool CanOpen2;

    private bool light1Activated;
    private bool light2Activated;
    private bool light3Activated;
    private bool light4Activated;
    private bool light5Activated;

    //Hackeo

    private bool CanMove;

    [SerializeField] private Text interactText; //texto interactuar con E
    [SerializeField] private Image progressBar;

    [SerializeField] private bool isInteractable;
    [SerializeField] private float fillAmount; //progreso del hackeo

    [SerializeField] private GameObject scrollbar;

    private void Awake()
    {
        InitialRotation = transform.rotation;
        InitialPos = transform.position;

        //Definir componentes(SpawnPoint, CharacterController y NavMesh)
        //SpawnPoint = FindObjectOfType<SpawnPointScript>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
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

        if (light4Activated == true && light5Activated == true) { CanOpen1 = true; }
        if (light1Activated == true && light2Activated == true && light3Activated == true) { CanOpen2 = true; }

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
    IEnumerator Distracted()
    {
        yield return new WaitForSeconds(ShootInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttackTag"))
        {
            //Debug.Log(transform.position);
            transform.SetPositionAndRotation(InitialPos, InitialRotation);
            //Debug.Log(transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Object1") && !light1Activated){
            ActivateHackingUI();
            Hacking1();
        }
        if (other.gameObject.CompareTag("Object2") && !light2Activated){
            ActivateHackingUI();
            Hacking2();
        }
        if (other.gameObject.CompareTag("Object3") && !light3Activated){
            ActivateHackingUI();
            Hacking3();
        }
        if (other.gameObject.CompareTag("Object4") && !light4Activated){
            ActivateHackingUI();
            Hacking4();
        }
        if (other.gameObject.CompareTag("Object5") && !light5Activated){
            ActivateHackingUI();
            Hacking5();
        }
        if (other.gameObject.CompareTag("FinalPC")){
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

    //private void Hacking(GameObject light bool lightActivated)
    //{
    //    if (isInteractable)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            fillAmount = 0f;
    //        }

    //        if (Input.GetKey(KeyCode.E))
    //        {
    //            scrollbar.SetActive(true);
    //            fillAmount += (Time.deltaTime / 6);
    //            progressBar.fillAmount = fillAmount;
    //            IsHacking = true;
    //            if (fillAmount >= 1f)
    //            {
    //                //Debug.Log("desactivar cosas");
    //                IsHacking = false;
    //                light.SetActive(true);
    //                lightActivated = true;
    //                DeactivateHackingUI();
    //            }
    //        }
    //        else
    //        {
    //            IsHacking = false;
    //            progressBar.fillAmount = 0f;
    //            fillAmount = 0f;
    //        }
    //    }
    //}
    private void Hacking1()
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
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    IsHacking = false;
                    light1.SetActive(true);
                    light1Activated = true;
                    DeactivateHackingUI();
                }
            }
            else
            {
                CanMove = true;
                IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }
    private void Hacking2()
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
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    light2.SetActive(true);
                    light2Activated = true;
                    DeactivateHackingUI();
                }
            }
            else
            {
                CanMove = true;
                IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }
    private void Hacking3()
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
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    light3.SetActive(true);
                    light3Activated = true;
                    DeactivateHackingUI();
                }
            }
            else
            {
                CanMove = true;
                IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }
    private void Hacking4()
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
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    light4.SetActive(true);
                    light4Activated = true;
                    DeactivateHackingUI();
                }
            }
            else
            {
                CanMove = true;
                IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }
    private void Hacking5()
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
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    light5.SetActive(true);
                    light5Activated = true;
                    DeactivateHackingUI();
                }
            }
            else
            {
                CanMove = true;
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
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 8);
                progressBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    //Debug.Log("desactivar cosas");
                    IsHacking = false;
                    DeactivateHackingUI();
                    GameManager.ChangeScene(1);
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

    private void ActivateHackingUI() {
        isInteractable = true;
        interactText.enabled = true;
    }
    private void DeactivateHackingUI() { 
        isInteractable = false;
        interactText.enabled = false;
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
}
