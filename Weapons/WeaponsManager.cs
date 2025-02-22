using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{
    public static WeaponsManager instance;
    public WeaponsListSO weaponsList;
    bool canChangeWeapon;
    [SerializeField] public UnityEvent onWeaponChange;
    [SerializeField] public UnityEvent onWeaponFire;


    [SerializeField] bool isRealoading;
    [SerializeField] bool canFire;
    [SerializeField] bool isFiring;

    RaycastHit hit;
    [SerializeField] GameObject shadow;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject raycastCamera;
    [SerializeField] GameObject crossHair;
    [SerializeField] GameObject reloadingPanel;

    [SerializeField] LayerMask raycastHitable;
    [SerializeField] IntSO killstreakMultiplier;


    [SerializeField] InputActionReference moveActionJoystick;
    float changeWeapon;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponsList.index = 0;
        crossHair.GetComponent<RawImage>().texture = weaponsList.weaponsList[weaponsList.index].crossHair;
        isRealoading = false;
        //WeaponSaveLoadManager.instance.LoadWeaponData();
    }

    // Update is called once per frame
    void Update()
    {
        RayCastFunc();
        ChangeWeapons();
    }
    void ChangeWeapons()
    {
        changeWeapon = moveActionJoystick.action.ReadValue<Vector2>().y;
        if (!isRealoading)
        {
            if (changeWeapon < 0.9f && changeWeapon > -0.9f)
            {
                canChangeWeapon = true;
            }
            else if (changeWeapon > 0.9f && canChangeWeapon)
            {
                canChangeWeapon = false;
                ChangeToNextPurchasedWeapon();
            }
            else if (changeWeapon < -0.9f && canChangeWeapon)
            {
                canChangeWeapon = false;
                ChangeToPreviousPurchasedWeapon();
            }
        }
    }
    void ChangeToNextPurchasedWeapon()
    {
        int nextIndex = (weaponsList.index + 1) % weaponsList.weaponsList.Count;
        while (!weaponsList.weaponsList[nextIndex].isUnlocked)
        {
            nextIndex = (nextIndex + 1) % weaponsList.weaponsList.Count;
        }
        weaponsList.index = nextIndex;
        SetWeapon();
    }
    void ChangeToPreviousPurchasedWeapon()
    {
        int prevIndex = (weaponsList.index - 1 + weaponsList.weaponsList.Count) % weaponsList.weaponsList.Count;
        while (!weaponsList.weaponsList[prevIndex].isUnlocked)
        {
            prevIndex = (prevIndex - 1 + weaponsList.weaponsList.Count) % weaponsList.weaponsList.Count;
        }
        weaponsList.index = prevIndex;
        SetWeapon();
    }
    void SetWeapon()
    {
        onWeaponChange.Invoke();
        crossHair.GetComponent<RawImage>().texture = weaponsList.weaponsList[weaponsList.index].crossHair;
    }
    void RayCastFunc()
    {
        if (Physics.Raycast(playerPos.transform.position, playerPos.transform.forward, 
                            out hit, 150, raycastHitable))
        {
            shadow.gameObject.SetActive(true);
            shadow.transform.position = hit.point;
            shadow.transform.localScale = new Vector3(Mathf.Clamp(hit.distance, 0, 0.4f), 0.1f, Mathf.Clamp(hit.distance, 0, 0.4f));
            raycastCamera.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+1.5f, hit.transform.position.z - 3);
            WeaponScope();
        }
        else
        {
            raycastCamera.transform.position = playerPos.position;
            shadow.gameObject.SetActive(false);
            crossHair.GetComponent<RawImage>().color = Color.white;
        }
    }
    void WeaponScope()
    {
        if (hit.transform.gameObject.tag == "Enemy")
        {
            crossHair.GetComponent<RawImage>().color = Color.red;
        }
        else if (hit.transform.gameObject.tag == "People")
        {
            crossHair.GetComponent<RawImage>().color = Color.green;
        }
        else if (hit.transform.gameObject.tag == "SpecialPower")
        {
            crossHair.GetComponent<RawImage>().color = Color.blue;

        }
    }
    public void FireWeapon()
    {
        if (!isFiring && !isRealoading && canFire && weaponsList.weaponsList[weaponsList.index].bulletsInClip > 0)
        {
            StartCoroutine(ShootWeapon());
        }
    }
    public void StopFiring()
    {
        canFire = false;
    }    
    public void StartFiring()
    {
        canFire = true;
    }
    IEnumerator ShootWeapon()
    {
        isFiring = true;
        weaponsList.weaponsList[weaponsList.index].bulletsInClip--;
        CheckHitRayCast();
        //UIManager.ShowAmountOfBullets  Camera.CameraShake
        onWeaponFire.Invoke();
        if (weaponsList.weaponsList[weaponsList.index].bulletsInClip == 0) StartCoroutine(ReloadWeapon());
        yield return new WaitForSeconds(weaponsList.weaponsList[weaponsList.index].timeBetweenShots);
        isFiring=false;
        FireWeapon();
    }
    IEnumerator ReloadWeapon()
    {
        isRealoading = true;
        reloadingPanel.SetActive(true);
        yield return new WaitForSeconds(weaponsList.weaponsList[weaponsList.index].reloadingTime);
        isRealoading = false;
        reloadingPanel.SetActive(false);
        weaponsList.weaponsList[weaponsList.index].bulletsInClip = weaponsList.weaponsList[weaponsList.index].clipSize;
        onWeaponChange.Invoke();
    }
    public void CallReloadWeapon()
    {
        StartCoroutine(ReloadWeapon());
    }
    void CheckHitRayCast()
    {
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                GameObject temp = hit.transform.gameObject;
                if (weaponsList.weaponsList[weaponsList.index].hasExplosion)
                {
                    Explosion(temp);
                }
                temp.GetComponent<TopDownEnemyEngine>().updateHealthBar(weaponsList.weaponsList[weaponsList.index].damage);
                if (temp.GetComponent<TopDownEnemyEngine>().enemyHP <= 0)
                {
                    hit.transform.gameObject.SetActive(false);
                    GameObject psSystemExplosion = CreateExplosionPool.SharedInstance.GetPooledObject(UnityEngine.Random.Range(0, CreateExplosionPool.SharedInstance.pooledObjects.Count));
                    StartCoroutine(SpecialPowersManager.Instance.CreateExplosion(psSystemExplosion, hit.collider));
                    EnemyManager.instance.amountOfEnemiesKilled++;
                    GameManager.instance.amountOfEnemiesKilledText.text = "Enemies Killed: " + EnemyManager.instance.amountOfEnemiesKilled.ToString();
                }
                killstreakMultiplier.value += 1;
            }
            else if (hit.transform.gameObject.tag == "People")
            {
                GameObject temp = hit.transform.gameObject;
                hit.transform.gameObject.SetActive(false);
                if (weaponsList.weaponsList[weaponsList.index].hasExplosion)
                {
                    Explosion(temp);
                }
                killstreakMultiplier.value = 0;
            }
            else if (hit.transform.gameObject.tag == "SpecialPower")
            {
                int powerIndex = hit.transform.GetComponent<SpecialPowerEngine>().listID;
                hit.transform.gameObject.SetActive(false);
                ActivateSpecialPower.SetSpecialPower(powerIndex);
            }
            else if (hit.transform.gameObject.tag == "ExplosiveBarrel")
            {
                GameObject explosion = SpecialPowersManager.Instance.BlackHawk.GetComponent<BlakcHawkMovement>().GetExplosions()[0];
                if (explosion != null)
                {
                    explosion = Instantiate(explosion);
                    explosion.transform.position = new Vector3(hit.transform.position.x, 5.78f, hit.transform.position.z);
                    explosion.transform.parent = GameObject.Find("Objects/Explosions").transform;
                    explosion.SetActive(true);
                    StartCoroutine(SpecialPowersManager.Instance.MakeExplosion(20 + SpecialPowersManager.Instance.specialPowersListSO.specialPowersList[(int)PlayerPowersManager.Explosion].upgradedValue, 0f));

                    Destroy(hit.transform.gameObject, 0.1f);
                    Destroy(explosion, 2.6f);
                }
            }
        }
        else
        {
            killstreakMultiplier.value = 0;
        }
    }


    void Explosion(GameObject temp)
    {
        GameObject explosion = Instantiate(weaponsList.weaponsList[weaponsList.index].explosionPS);
        explosion.transform.position = hit.transform.position;
        Destroy(explosion,2);
        Collider[] colliders = Physics.OverlapSphere(temp.transform.position, weaponsList.weaponsList[weaponsList.index].explosionSize);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy" && hit != null)
            {
                temp.GetComponent<TopDownEnemyEngine>().updateHealthBar(weaponsList.weaponsList[weaponsList.index].explosionDamage);
                if (temp.GetComponent<TopDownEnemyEngine>().enemyHP <= 0)
                {
                    hit.transform.gameObject.SetActive(false);
                    GameObject psSystemExplosion = CreateExplosionPool.SharedInstance.GetPooledObject(UnityEngine.Random.Range(0, CreateExplosionPool.SharedInstance.pooledObjects.Count));
                    StartCoroutine(SpecialPowersManager.Instance.CreateExplosion(psSystemExplosion, hit));
                    EnemyManager.instance.amountOfEnemiesKilled++;
                    GameManager.instance.amountOfEnemiesKilledText.text = "Enemies Killed: " + EnemyManager.instance.amountOfEnemiesKilled.ToString();
                }
            }
            else if (hit.transform.gameObject.tag == "People" && hit != null)
            {
                GameObject psSystemExplosion = CreateExplosionPool.SharedInstance.GetPooledObject(UnityEngine.Random.Range(0, CreateExplosionPool.SharedInstance.pooledObjects.Count));
                StartCoroutine(SpecialPowersManager.Instance.CreateExplosion(psSystemExplosion, hit));
            }
        }
    }
}
