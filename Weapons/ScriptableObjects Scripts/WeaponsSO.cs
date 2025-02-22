using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/SpawnWeapon", order = 1)]

public class WeaponsSO : ScriptableObject
{
    [Header("Basic")]
    public string weaponName;
    public int clipSize;
    public int bulletsInClip;
    public int damage;
    public bool isAuto;

    public bool hasMaxRange;
    public float range;

    [Header("Time")]
    public float reloadingTime;
    public float timeBetweenShots;

    [Header("Explosion")]
    public bool hasExplosion;
    public float explosionSize;
    public int explosionDamage;
    public GameObject explosionPS;

    [Header("Visuals")]
    public Texture crossHair;
    public GameObject weaponObject;
    public GameObject muzzleFlashPS;
    public float cameraShakeStrength;
    public float cameraShakeLength;
    public Sprite weaponIcon;


    [Header("Sounds")]
    public AudioClip[] shootSound;

    [Header("Shop Details")]
    public int purchasePrice;
    public bool isUnlocked;
    public int upgradeDamagePrice = 50;
    public int upgradeDamageCount = 0;
    public int upgradeClipSizePrice = 50;
    public int upgradeClipSizeCount = 0;
    public int upgradeReloadingTimePrice = 50;
    public int upgradeReloadingTimeCount = 0;
    public int upgradeTimeBetweenShotsPrice = 50;
    public int upgradeTimeBetweenShotsCount = 0;
    public int upgradeExplosionDamagePrice = 50;
    public int upgradeExplosionDamageCount = 0;
}
