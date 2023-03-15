using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PauseMenu pausemenu;

    private static GameManager instance;
    public static GameManager Instance() {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool IsPaused() {
        return pausemenu.GameIsPaused;
    }
}
