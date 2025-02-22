using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject shadow;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject raycastCamera;

    [SerializeField] LayerMask raycastHitable;
    // Start is called before the first frame update
    void Start()
    {
        shadow.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit,150, raycastHitable))
        {
            shadow.gameObject.SetActive(true);
            shadow.transform.position = hit.point;
            shadow.transform.localScale = new Vector3(Mathf.Clamp(hit.distance, 0, 0.7f), 0.1f, Mathf.Clamp(hit.distance, 0, 0.7f));
            raycastCamera.transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z - 3);

        }
        else
        {
            raycastCamera.transform.position = playerPos.position;
            shadow.gameObject.SetActive(false);
        }

    }

    public void DestroyHitObject()
    {
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                GameObject temp = hit.transform.gameObject;
                //temp.GetComponent<TopDownEnemyEngine>().updateHealthBar();
                //temp.GetComponent<TopDownEnemyEngine>().TakeDamage(1);
                if (temp.GetComponent<TopDownEnemyEngine>().enemyHP <= 0)
                {
                    //GameObject psSystemExplosion = Instantiate(SpecialPowersManager.Instance.expPS, hit.transform.position, hit.transform.rotation);
                    hit.transform.gameObject.SetActive(false);
                    GameObject psSystemExplosion = CreateExplosionPool.SharedInstance.GetPooledObject(Random.Range(0, CreateExplosionPool.SharedInstance.pooledObjects.Count));
                    StartCoroutine(SpecialPowersManager.Instance.CreateExplosion(psSystemExplosion,hit.collider));
                    //Destroy(psSystemExplosion, 1);
                    //EnemyManager.instance.amountOfEnemiesToMake--;
                    EnemyManager.instance.amountOfEnemiesKilled++;
                    GameManager.instance.amountOfEnemiesKilledText.text = "Enemies Killed: " + EnemyManager.instance.amountOfEnemiesKilled.ToString();
                }
            }
           else if (hit.transform.gameObject.tag == "People")
            {
                hit.transform.gameObject.SetActive(false);
                //point-=10;
            }
        }
    }
}
