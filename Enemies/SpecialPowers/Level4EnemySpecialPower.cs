using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4EnemySpecialPower : MonoBehaviour
{
    float addLife;
    private void OnEnable()
    {
        StartCoroutine(AddLifeToEnemiesAround());
    }

    IEnumerator AddLifeToEnemiesAround()
    {
        addLife = Random.Range(0, 3);
        if (addLife == 0)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, 5);
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.tag == "Enemy" && hit != null)
                {
                    hit.gameObject.GetComponent<TopDownEnemyEngine>().enemyHP++;
                }
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(AddLifeToEnemiesAround());
    }

    private void OnDisable()
    {
        StopCoroutine(AddLifeToEnemiesAround());
    }
}
