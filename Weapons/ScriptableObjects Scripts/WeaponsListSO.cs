using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsList", menuName = "ScriptableObjects/WeaponList", order = 2)]
public class WeaponsListSO : ScriptableObject
{
    public List<WeaponsSO> weaponsList;
    public int index;

    private void OnEnable()
    {
        for (int i = 0; i < weaponsList.Count; i++)
        {
            weaponsList[i].bulletsInClip = weaponsList[i].clipSize;
        }
    }
}
