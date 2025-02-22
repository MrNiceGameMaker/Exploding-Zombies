using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialPowersList", menuName = "ScriptableObjects/SpecialPowersList", order = 2)]
public class SpecialPowersListSO : ScriptableObject
{
    public List<SpecialPowerSO> specialPowersList;
}
