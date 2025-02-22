using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2EnemySpecialPower : MonoBehaviour
{
    TopDownEnemyEngine topDownEnemyEngineRef;
    // Start is called before the first frame update
    void Start()
    {
        topDownEnemyEngineRef = GetComponent<TopDownEnemyEngine>();

    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(AddHpToEnemy), 0, 2);
    }
    void AddHpToEnemy()
    {
        if(topDownEnemyEngineRef != null)
        {
            if(topDownEnemyEngineRef.enemyHP > 0)
            {
                int doFillHP = Random.Range(0, 4);
                if(doFillHP == 1) 
                {
                    topDownEnemyEngineRef.enemyHP++;
                    topDownEnemyEngineRef.maxEnemyHP++;
                }
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(AddHpToEnemy));
    }
}
