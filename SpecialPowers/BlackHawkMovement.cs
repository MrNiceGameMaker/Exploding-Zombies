using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlakcHawkMovement : MonoBehaviour
{
    // -11.7, 42.9, 108
    [SerializeField] Vector3 startPos;

    [SerializeField] GameObject[] explosionsPs;
/*    private void Awake()
    {
        startPos = transform.position;
    }*/
    private void OnEnable()
    {
        StartCoroutine(StartExplosions());
        Invoke(nameof(DisableBlackHawk), 3);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 53 *Time.deltaTime);
    }
    void DisableBlackHawk()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        transform.position = startPos;
    }
    IEnumerator StartExplosions()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < explosionsPs.Length; i++)
        {
            explosionsPs[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public GameObject[] GetExplosions()
    {
        return explosionsPs;
    }
}

