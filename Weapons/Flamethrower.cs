using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    WeaponsManager weaponsManager;
    Transform flames;
    private void Awake()
    {
        weaponsManager = FindObjectOfType<WeaponsManager>();
    }

    private void OnEnable()
    {
        if (flames == null)
        {
            flames = gameObject.transform.GetChild(0);
            flames.gameObject.SetActive(true);
        }
        weaponsManager.onWeaponFire.AddListener(ShootFlamesFunc);
    }
    private void OnDisable()
    {
        weaponsManager.onWeaponFire.RemoveListener(ShootFlamesFunc);
    }
    void ShootFlamesFunc()
    {
        flames.gameObject.GetComponent<ParticleSystem>().Play();
    }
}
