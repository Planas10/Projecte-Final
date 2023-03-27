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

    private bool light1Activated = false;
    private bool light2Activated = false;
    private bool light3Activated = false;
    private bool light4Activated = false;
    private bool light5Activated = false;

    //Hackeo

    public Text interactText;
    public Image progressBar;

    private bool isInteractable;
    private float fillAmount;

    [SerializeField] private GameObject scrollbar;


    void Start()
    {
        interactText.enabled = false;
        scrollbar.SetActive(false); 
        progressBar.fillAmount = 0f;
        isInteractable = false;
        fillAmount = 0f;

    }


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
        //Debug.Log(fillAmount);
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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Object1") && !light1Activated){
            isInteractable = true;
            interactText.enabled = true;
            Hacking(light1, scrollbar, interactText, light1Activated);
            Debug.Log(isInteractable + "1");
            Debug.Log(light1Activated + "1");
        }
        if (other.gameObject.CompareTag("Object2") && !light2Activated){
            isInteractable = true;
            interactText.enabled = true;
            Hacking(light2, scrollbar, interactText, light2Activated);
            Debug.Log(isInteractable + "2");
            Debug.Log(light2Activated + "2");
        }
        if (other.gameObject.CompareTag("Object3") && !light3Activated){
            isInteractable = true;
            interactText.enabled = true;
            Hacking(light3, scrollbar, interactText, light3Activated);
            Debug.Log(isInteractable + "3");
            Debug.Log(light3Activated + "3");
        }
        if (other.gameObject.CompareTag("Object4") && !light4Activated){
            isInteractable = true;
            interactText.enabled = true;
            Hacking(light4, scrollbar, interactText, light4Activated);
            Debug.Log(isInteractable + "4");
            Debug.Log(light4Activated + "4");
        }
        if (other.gameObject.CompareTag("Object5") && !light5Activated) {
            isInteractable = true;
            interactText.enabled = true;
            Hacking(light5, scrollbar, interactText, light5Activated);
            Debug.Log(isInteractable + "5");
            Debug.Log(light5Activated + "5");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object1") ||
            other.CompareTag("Object2") ||
            other.CompareTag("Object3") ||
            other.CompareTag("Object4") ||
            other.CompareTag("Object5")) { DeactivateHackingUI(); }
    }

    private void Hacking(GameObject light, GameObject scrollBar, Text texto, bool lightActivated)
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
                fillAmount += (Time.deltaTime / 5);
                progressBar.fillAmount = fillAmount;
                IsHacking = true;
                if (fillAmount >= 1f)
                {
                    Debug.Log("desactivar cosas");
                    IsHacking = false;
                    light.SetActive(true);
                    lightActivated = true;
                    isInteractable = false;
                    texto.enabled = false;
                    progressBar.enabled = false;
                    progressBar.fillAmount = 0f;
                    fillAmount = 0f;
                    scrollBar.SetActive(false);
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
