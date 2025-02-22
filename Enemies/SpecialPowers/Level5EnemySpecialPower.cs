using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5EnemySpecialPower : MonoBehaviour
{
    [SerializeField] IntSO level;
    [SerializeField] IntSO amountOfEnemiesLeft;
    private void OnEnable()
    {
        InvokeRepeating(nameof(CreateNewEnemy), 0, 2);
    }

    void CreateNewEnemy()
    {
        amountOfEnemiesLeft.value++;
        EnemyManager.instance.CreateEnemy();
        /*int randomNum = Random.Range(0, 100);
        int enemyIndex = 0;
        for (int i = 1; i <CreateEnemyPool.SharedInstance.objectToPool.Length; i++)
        {
            if (randomNum < (70 - level.value))
            {
                break;
            }
            else randomNum = Random.Range(0, 100);
            enemyIndex = i;
        }
        GameObject temp = CreateEnemyPool.SharedInstance.GetPooledObject(enemyIndex);
        amountOfEnemiesLeft.value++;
        temp.SetActive(true);
        temp.transform.position = new Vector3(transform.position.x + Random.Range(-2f, 2f), -0.2f, transform.position.z + Random.Range(-2f, 2f));
        temp.transform.rotation = transform.rotation;
        temp.GetComponent<TopDownEnemyEngine>().enemyHP = enemyIndex + 1;*/
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(CreateNewEnemy));
    }
}
