using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLuncherEngine : MonoBehaviour
{
    WeaponsManager weaponsManager;
    [SerializeField] Vector3 startPos;
    private void Awake()
    {
        weaponsManager = FindObjectOfType<WeaponsManager>();
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void Start()
    {
        startPos = transform.localPosition;
    }
    private void OnEnable()
    {
        weaponsManager.onWeaponFire.AddListener(ShootBulletFunc);
        weaponsManager.onWeaponChange.AddListener(ReturnToStartPos);
    }
    private void OnDisable()
    {
        weaponsManager.onWeaponFire.RemoveListener(ShootBulletFunc);
        weaponsManager.onWeaponChange.RemoveListener(ReturnToStartPos);


    }
    void ShootBulletFunc()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 2500);
    }
    void ReturnToStartPos()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.localPosition = startPos;
    }

}
