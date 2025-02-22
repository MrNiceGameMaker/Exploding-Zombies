using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSaveLoadManager : MonoBehaviour
{
    public static WeaponSaveLoadManager instance;
    public WeaponsListSO weaponsListSO;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            GameObject dontDestroyOnLoadObject = GameObject.Find("DontDestroyOnLoad");
            if (dontDestroyOnLoadObject != null)
            {
                transform.parent = dontDestroyOnLoadObject.transform;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        //ES3.DeleteKey("weaponDataList");
    }

    public void SaveWeaponData()
    {
        List<WeaponData> weaponDataList = new List<WeaponData>();
        foreach (var weapon in weaponsListSO.weaponsList)
        {
            WeaponData weaponData = new WeaponData();
            weaponData.weaponName = weapon.weaponName;
            weaponData.isUnlocked = weapon.isUnlocked;
            weaponData.damage = weapon.damage;
            weaponData.clipSize = weapon.clipSize;
            weaponData.reloadingTime = weapon.reloadingTime;
            weaponData.timeBetweenShots = weapon.timeBetweenShots;
            weaponData.explosionDamage = weapon.explosionDamage;
            weaponData.upgradeDamagePrice = weapon.upgradeDamagePrice;
            weaponData.upgradeClipSizePrice = weapon.upgradeClipSizePrice;
            weaponData.upgradeReloadingTimePrice = weapon.upgradeReloadingTimePrice;
            weaponData.upgradeTimeBetweenShotsPrice = weapon.upgradeTimeBetweenShotsPrice;
            weaponData.upgradeExplosionDamagePrice = weapon.upgradeExplosionDamagePrice;
            weaponData.upgradeDamageCount = weapon.upgradeDamageCount;
            weaponData.upgradeClipSizeCount = weapon.upgradeClipSizeCount;
            weaponData.upgradeReloadingTimeCount = weapon.upgradeReloadingTimeCount;
            weaponData.upgradeTimeBetweenShotsCount = weapon.upgradeTimeBetweenShotsCount;
            weaponData.upgradeExplosionDamageCount = weapon.upgradeExplosionDamageCount;

            weaponDataList.Add(weaponData);
        }
        ES3.Save<List<WeaponData>>("weaponDataList", weaponDataList) ;
    }

    public void LoadWeaponData()
    {
        if (ES3.KeyExists("weaponDataList"))
        {
            List<WeaponData> weaponDataList = ES3.Load<List<WeaponData>>("weaponDataList");

            for (int i = 0; i < weaponDataList.Count; i++)
            {
                weaponsListSO.weaponsList[i].isUnlocked = weaponDataList[i].isUnlocked;
                weaponsListSO.weaponsList[i].damage = weaponDataList[i].damage;
                weaponsListSO.weaponsList[i].clipSize = weaponDataList[i].clipSize;
                weaponsListSO.weaponsList[i].reloadingTime = weaponDataList[i].reloadingTime;
                weaponsListSO.weaponsList[i].timeBetweenShots = weaponDataList[i].timeBetweenShots;
                weaponsListSO.weaponsList[i].upgradeDamagePrice = weaponDataList[i].upgradeDamagePrice;
                weaponsListSO.weaponsList[i].upgradeClipSizePrice = weaponDataList[i].upgradeClipSizePrice;
                weaponsListSO.weaponsList[i].upgradeReloadingTimePrice = weaponDataList[i].upgradeReloadingTimePrice;
                weaponsListSO.weaponsList[i].upgradeTimeBetweenShotsPrice = weaponDataList[i].upgradeTimeBetweenShotsPrice;
                weaponsListSO.weaponsList[i].upgradeDamageCount = weaponDataList[i].upgradeDamageCount;
                weaponsListSO.weaponsList[i].upgradeClipSizeCount = weaponDataList[i].upgradeClipSizeCount;
                weaponsListSO.weaponsList[i].upgradeReloadingTimeCount = weaponDataList[i].upgradeReloadingTimeCount;
                weaponsListSO.weaponsList[i].upgradeTimeBetweenShotsCount = weaponDataList[i].upgradeTimeBetweenShotsCount;
                weaponsListSO.weaponsList[i].explosionDamage = weaponDataList[i].explosionDamage;
                weaponsListSO.weaponsList[i].upgradeExplosionDamagePrice = weaponDataList[i].upgradeExplosionDamagePrice;
                weaponsListSO.weaponsList[i].upgradeExplosionDamageCount = weaponDataList[i].upgradeExplosionDamageCount;
            }
        }
    }
    public class WeaponData
    {
        public string weaponName;
        public int clipSize;
        public int damage;
        public int explosionDamage;
        public float reloadingTime;
        public float timeBetweenShots;
        public bool isUnlocked;
        public int upgradeDamagePrice;
        public int upgradeDamageCount;
        public int upgradeClipSizePrice;
        public int upgradeClipSizeCount;
        public int upgradeReloadingTimePrice;
        public int upgradeReloadingTimeCount;
        public int upgradeTimeBetweenShotsPrice;
        public int upgradeTimeBetweenShotsCount;
        public int upgradeExplosionDamagePrice;
        public int upgradeExplosionDamageCount;
    }
}