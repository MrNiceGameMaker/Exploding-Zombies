using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PointsManager : MonoBehaviour
{
    [SerializeField] FloatSO points;
    [SerializeField] IntSO killstreakMultiplier;
    [SerializeField] UnityEvent<float, Vector3> addPointsOnZobieKill;
    private void Start()
    {
        points.value = 0;
        killstreakMultiplier.value = 0;
    }

    public void AddPointsForKill(Vector3 enemyPos, int enemyLevel)
    {
        float pointToAdd;
        if (killstreakMultiplier.value > 0)
        {
            pointToAdd = ((enemyLevel + 1) * 10) * (killstreakMultiplier.value * 0.1f + 1);
        }
        else
        {
            pointToAdd = ((enemyLevel + 1) * 10);
        }
        points.value += pointToAdd;
        addPointsOnZobieKill.Invoke(pointToAdd, enemyPos);
    }
}
 