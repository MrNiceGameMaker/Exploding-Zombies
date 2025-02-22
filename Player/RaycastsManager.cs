using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastsManager : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject[] shadow = new GameObject[4];

    [SerializeField] GameObject[] rayCasts = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < shadow.Length; i++)
        {
            shadow[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rayCasts.Length; i++)
        {
            if (Physics.Raycast(rayCasts[i].transform.position, rayCasts[i].transform.forward, out hit, 5))
            {
                shadow[i].gameObject.SetActive(true);
                shadow[i].transform.position = hit.point;
            }
            else
            {
                shadow[i].gameObject.SetActive(false);
            }
        }

    }
}
