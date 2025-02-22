using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectsManager : MonoBehaviour
{
    public WeaponsListSO weaponsListSO;

    public List<GameObject> weaponPrefabList = new List<GameObject>();

    [SerializeField] Transform playerWeapons;
    private void Start()
    {
        for (int i = 0; i < weaponsListSO.weaponsList.Count; i++)
        {
            GameObject temp =  Instantiate(weaponsListSO.weaponsList[i].weaponObject, playerWeapons);
            weaponPrefabList.Add(temp);
            weaponPrefabList[i].SetActive(false);
        }
        weaponPrefabList[weaponsListSO.index].SetActive(true);
    }

    public void OnWeaponChange()
    {
        for(int i = 0;i < weaponPrefabList.Count; i++) 
        {
            weaponPrefabList[i].SetActive(false);
        }
        weaponPrefabList[weaponsListSO.index].SetActive(true);
    }
}
