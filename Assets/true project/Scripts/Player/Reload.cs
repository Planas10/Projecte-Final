using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    [Header ("Audio")]
    public AudioSource audioReload;

    [Header("Reload parameters")]
    public int MaxAmmo;
    public int RemainingAmmo;
    public bool IsReloading = false;
    public float reloadTime;
    public bool canReloadNow;

    [Header ("Canvas UI")]
    public Image bulletProgressBar;
    public float bulletFillAmount = 1f;

    private void Awake()
    {
        //audioReload = GetComponent<AudioSource>();        
        bulletProgressBar.fillAmount = bulletFillAmount;
        RemainingAmmo = MaxAmmo;
    }

    private void Update()
    {
        if (RemainingAmmo < MaxAmmo)
            canReloadNow = true;
        else
            canReloadNow = false;
    }

    public IEnumerator ReloadWeapon()
    {
        if (canReloadNow)
        {
            IsReloading = true;
            audioReload.Play();
            yield return new WaitForSeconds(reloadTime);
            RemainingAmmo = MaxAmmo;
            bulletProgressBar.fillAmount = bulletFillAmount;
            IsReloading = false;
        }
    }
}
