using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    [SerializeField] List<GameObject> zones = new List<GameObject>();
    [SerializeField] Transform enviorment;

    [SerializeField] IntSO currentZone;
    GameObject tempZone;
    // Start is called before the first frame update
    void Start()
    {
        currentZone.value = 0;
        tempZone = Instantiate(zones[currentZone.value]);
        tempZone.SetActive(true);
        tempZone.transform.parent = enviorment;
        tempZone.transform.localPosition = Vector3.zero;
    }
    
    public void ChangeZone()
    {
        if(currentZone.value < zones.Count)
        {
            MakeZone(currentZone.value);
        }
        else
        {
            int zoneTomake = Random.Range(0, zones.Count);
            MakeZone(zoneTomake);
        }
    }

    void MakeZone(int zoneTomake)
    {
        Destroy(tempZone.gameObject);
        tempZone = Instantiate(zones[zoneTomake]);
        tempZone.SetActive(true);
        tempZone.transform.parent = enviorment;
        tempZone.transform.localPosition = Vector3.zero;
    }
}
