using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableExplosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(DisableObject), 2.5f);
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
