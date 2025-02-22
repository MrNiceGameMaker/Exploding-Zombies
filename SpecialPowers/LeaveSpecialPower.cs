using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSpecialPower : MonoBehaviour
{
    [SerializeField] int chanceToLeavePowerOnEnemyDeath;
    public void ChanceToLeaveSpecialPower(Vector3 powerPos, int enemyLevel)
    {
        int chanceToLeavePower = Random.Range(0, chanceToLeavePowerOnEnemyDeath);
        if (chanceToLeavePower == 0)
        {    
            List<int> unlockedPowers = new List<int>();
            for (int i = 0; i < CreateSpecialPowersPool.SharedInstance.specialPowersList.specialPowersList.Count; i++)
            {
                if (CreateSpecialPowersPool.SharedInstance.specialPowersList.specialPowersList[i].isUnlocked)
                {
                    unlockedPowers.Add(i);
                }
            }
            if (unlockedPowers.Count > 0)
            {
                int specialPowerToMake = unlockedPowers[Random.Range(0, unlockedPowers.Count)];
                GameObject temp = CreateSpecialPowersPool.SharedInstance.GetPooledObject(specialPowerToMake);
                if (temp != null)
                {
                    temp.SetActive(true);
                    temp.transform.position = new Vector3(powerPos.x, 1.5f, powerPos.z);
                    temp.transform.localScale = new Vector3(4, 4, 4);
                }
            }
        }
    }
}
