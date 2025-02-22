using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashEngine : MonoBehaviour
{
    WeaponsManager weaponsManager;
    ParticleSystem muzzleFlash;
    private void Awake()
    {
        weaponsManager = FindObjectOfType<WeaponsManager>();
        muzzleFlash = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        weaponsManager.onWeaponFire.AddListener(ActivateMuzzleFlashPS);
        weaponsManager.onWeaponChange.AddListener(DeActivateMuzzleFlashPS);
    }
    private void OnDisable()
    {
        weaponsManager.onWeaponFire.RemoveListener(ActivateMuzzleFlashPS);
        weaponsManager.onWeaponChange.RemoveListener(DeActivateMuzzleFlashPS);
    }
    void ActivateMuzzleFlashPS()
    {
        muzzleFlash.Play();
    }
    void DeActivateMuzzleFlashPS()
    {
        muzzleFlash.Stop();
    }
}
