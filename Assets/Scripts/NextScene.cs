using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{


    void OnCollisionEnter(){
        //Debug.LogError("collisión");
        SceneManager.LoadScene("LVL2");

    }
}
