using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level1EnemySpecialPower : MonoBehaviour
{
    int isSpeeding;

    private void OnEnable()
    {

        StartCoroutine(SpeedUpEnemy());
    }
    IEnumerator SpeedUpEnemy()
    {
        isSpeeding = Random.Range(0, 2);
        float oldSpeed = GetComponent<TopDownEnemyEngine>().speed;
        if (isSpeeding == 0)
        {
            GetComponent<TopDownEnemyEngine>().speed *= 2;
        }
        yield return new WaitForSeconds(1);
        GetComponent<TopDownEnemyEngine>().speed = oldSpeed;
        StartCoroutine(SpeedUpEnemy());

    }
    private void OnDisable()
    {
        StopCoroutine(SpeedUpEnemy());
    }
}
