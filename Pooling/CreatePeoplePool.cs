using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePeoplePool : MonoBehaviour
{
    public static CreatePeoplePool SharedInstance;

    public List<List<GameObject>> pooledObjects;
    public GameObject[] objectToPool;
    int[] amountToPool;
    public int poolSize;
    void Awake()
    {
        //makes the size of all the pools put together twice the size of amount of max road fregments 
        poolSize = 2;
        amountToPool = new int[objectToPool.Length];
        // pool 0 is empty because shape cannot be drawn from it
        //amountToPool[0] = 0;
        //resets each pool to the start size
        for (int i = 0; i < amountToPool.Length; i++)
        {
            amountToPool[i] = poolSize;
        }
        SharedInstance = this;
        pooledObjects = new List<List<GameObject>>();
        //pooledObjects.Add(null);
        GameObject tmp;
        for (int z = 0; z < objectToPool.Length; z++)
        {
            // makes a temp pool to store all the objects in so it can be added to the pool of pools
            List<GameObject> tempPool = new List<GameObject>();

            for (int i = 0; i < amountToPool[z]; i++)
            {
                tmp = Instantiate(objectToPool[z]);
                tmp.SetActive(false);
                tempPool.Add(tmp);
            }
            pooledObjects.Add(tempPool);
        }
    }
    public GameObject GetPooledObject(int poolNumber)
    {
        //returns an object from the pool sent via pool number
        List<GameObject> tempPool = pooledObjects[poolNumber];
        for (int i = 0; i < amountToPool[poolNumber]; i++)
        {
            if (!tempPool[i].activeInHierarchy)
            {
                return tempPool[i];
            }
        }
        return FillPoll(poolNumber);
    }
    public GameObject FillPoll(int poolNumber)
    {
        // if pool is empty creates a new object and adds it to the corret pool. also sends it back to use
        List<GameObject> tempPool = pooledObjects[poolNumber];

        GameObject tmp;
        tmp = Instantiate(objectToPool[poolNumber]);
        tempPool.Add(tmp);
        amountToPool[poolNumber] = pooledObjects[poolNumber].Count;
        return tempPool[tempPool.Count - 1];
    }
}
