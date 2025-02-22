using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialPower", menuName = "ScriptableObjects/SpecialPower", order = 1)]
public class SpecialPowerSO : ScriptableObject
{
    [Header("Basic")]
    public string powerName;
    public GameObject powerObject;

    [Header("Shop Details")]
    public int purchasePrice;
    public bool isUnlocked;

    [Header("Upgrade Details")]
    public int upgradePrice;
    public float upgradedValue;

    [Header("Visuals")]
    public Sprite powerIcon;
}
