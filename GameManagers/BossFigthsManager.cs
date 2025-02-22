using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFigthsManager : MonoBehaviour
{
    [SerializeField] List<BossesSO> bossesList;
    [SerializeField] IntSO CurrentZone;
    [SerializeField] IntSO amountOfEnemiesLeftInWave;
    public void InstantiateBossFight()
    {
        if(CurrentZone.value < bossesList.Count) 
        {
            for (int i = 0; i < bossesList[CurrentZone.value].bossesIndex.Count; i++)
            {
                MakeBoss(CurrentZone.value,i);
            }
        }else
        {
            int randomAmountOfBosses = Random.Range(1, CurrentZone.value);
            for (int i = 0; i < randomAmountOfBosses; i++)
            {
                int randomBoss = Random.Range(0, bossesList.Count);
                for (int j = 0; j < bossesList[randomBoss].bossesIndex.Count; j++)
                {
                    MakeBoss(randomBoss,j);
                }
            }
        }

    }
    void MakeBoss(int zoneBoss, int i)
    {
        GameObject temp = CreateEnemyPool.SharedInstance.GetPooledObject(bossesList[zoneBoss].bossesIndex[i] - 1);
        temp.GetComponent<TopDownEnemyEngine>().enemyHP = bossesList[zoneBoss].bossesIndex[i];
        temp.SetActive(true);
        temp.transform.position = new Vector3(Random.Range(-8f, 8f), -0.2f, 18);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newRotationY = currentRotation.y + 180f;
        temp.transform.rotation = Quaternion.Euler(currentRotation.x, newRotationY, currentRotation.z);
        amountOfEnemiesLeftInWave.value++;
    }
}
