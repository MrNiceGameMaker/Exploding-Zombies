using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] MeshRenderer floorMeshRenderer;
    [SerializeField] LavaModeManager lavaModeManager;
    [SerializeField] float timeInLaveMode;
    [SerializeField] Color startColor;

    private void Start()
    {
        startColor = floorMeshRenderer.material.color;
    }
    // Update is called once per frame
    void Update()
    {
        if (lavaModeManager == LavaModeManager.EnterLavaMode)
        {
            StartCoroutine(ChangeFloorColor(timeInLaveMode));
            lavaModeManager = LavaModeManager.StayInLavaMode;
        }
        else if (lavaModeManager == LavaModeManager.NotInLaveMode) 
        {
            floorMeshRenderer.material.color = startColor;
        }
    }

    IEnumerator ChangeFloorColor(float amountOfTimeInLaveMode)
    {
        if (amountOfTimeInLaveMode > 0)
        {
            yield return new WaitForSeconds(0.1f);
            floorMeshRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            floorMeshRenderer.material.color = Color.white;
            StartCoroutine(ChangeFloorColor(amountOfTimeInLaveMode-0.2f));
        }else
        {
            lavaModeManager = LavaModeManager.NotInLaveMode;
        }

    }
}
