using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level3EnemySpecialPower : MonoBehaviour
{
    float isSpeeding;
    private void OnEnable()
    {
        StartCoroutine(AddSpeedToEnemiesAround());
    }

    IEnumerator AddSpeedToEnemiesAround()
    {
        isSpeeding = Random.Range(0, 2);
        if (isSpeeding == 0)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, 5);
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.tag == "Enemy" && hit != null)
                {
                    hit.gameObject.GetComponent<TopDownEnemyEngine>().speed *= 2;
                    yield return new WaitForSeconds(1);
                    hit.gameObject.GetComponent<TopDownEnemyEngine>().speed /= 2;
                }
            }
        }
        StartCoroutine(AddSpeedToEnemiesAround());
    }

    private void OnDisable()
    {
        StopCoroutine(AddSpeedToEnemiesAround());
    }
}
