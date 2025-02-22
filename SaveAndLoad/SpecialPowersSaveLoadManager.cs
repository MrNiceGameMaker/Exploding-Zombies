using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPowersSaveLoadManager : MonoBehaviour
{
    public static SpecialPowersSaveLoadManager instance;
    public SpecialPowersListSO specialPowersListSO;

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
    }

    public void SaveSpecialPowerData()
    {
        List<SpecialPowerData> specialPowerDataList = new List<SpecialPowerData>();
        foreach (var specialPower in specialPowersListSO.specialPowersList)
        {
            SpecialPowerData specialPowerData = new SpecialPowerData
            {
                powerName = specialPower.powerName,
                isUnlocked = specialPower.isUnlocked,
                purchasePrice = specialPower.purchasePrice,
                upgradePrice = specialPower.upgradePrice,
                upgradedValue = specialPower.upgradedValue
            };
            specialPowerDataList.Add(specialPowerData);
        }
        ES3.Save("specialPowerDataList", specialPowerDataList);
    }

    public void LoadSpecialPowerData()
    {
        if (ES3.KeyExists("specialPowerDataList"))
        {
            List<SpecialPowerData> specialPowerDataList = ES3.Load<List<SpecialPowerData>>("specialPowerDataList");

            for (int i = 0; i < specialPowerDataList.Count; i++)
            {
                specialPowersListSO.specialPowersList[i].powerName = specialPowerDataList[i].powerName;
                specialPowersListSO.specialPowersList[i].isUnlocked = specialPowerDataList[i].isUnlocked;
                specialPowersListSO.specialPowersList[i].purchasePrice = specialPowerDataList[i].purchasePrice;
                specialPowersListSO.specialPowersList[i].upgradePrice = specialPowerDataList[i].upgradePrice;
                specialPowersListSO.specialPowersList[i].upgradedValue = specialPowerDataList[i].upgradedValue;
            }
        }
    }
        [System.Serializable]
    public class SpecialPowerData
    {
        public string powerName;
        public bool isUnlocked;
        public int purchasePrice;
        public int upgradePrice;
        public float upgradedValue;
    }
}
