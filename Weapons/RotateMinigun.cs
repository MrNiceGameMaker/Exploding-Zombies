using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMinigun : MonoBehaviour
{
    WeaponsManager weaponsManager;
    private void Awake()
    {
        weaponsManager = FindObjectOfType<WeaponsManager>();
    }
    private void OnEnable()
    {
        weaponsManager.onWeaponFire.AddListener(RotateBarrel);
    }
    private void OnDisable()
    {
        weaponsManager.onWeaponFire.RemoveListener(RotateBarrel);
    }

    void RotateBarrel()
    {
        transform.Rotate(0, 0, 45);
    }
}
