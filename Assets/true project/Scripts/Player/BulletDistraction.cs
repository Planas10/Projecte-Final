using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDistraction : MonoBehaviour
{
    private Vector3 CurrentSize;
    private Vector3 ScaleChange;

    public bool CanDie;

    public bullet bulletS;

    private void Awake()
    {
        CanDie = false;
        ScaleChange = new Vector3(0.75f, 0.75f, 0.75f);
        CurrentSize = transform.localScale;
    }

    private void Update()
    {
        if (bulletS.CanActivate && CurrentSize.x < 2f)
        {
            transform.localScale += ScaleChange * Time.deltaTime;
            CurrentSize = transform.localScale;
        }
        if (CurrentSize.x >= 2f)
        {
            CanDie = true;
        }
    }
}
