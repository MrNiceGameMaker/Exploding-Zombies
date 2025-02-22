using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaves : MonoBehaviour
{
    private void Awake()
    {
        WeaponSaveLoadManager.instance.LoadWeaponData();
        SpecialPowersSaveLoadManager.instance.LoadSpecialPowerData();
    }
}
