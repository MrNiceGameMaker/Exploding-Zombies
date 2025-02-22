using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemy : MonoBehaviour
{
    Rigidbody rb;
    float speed;
    bool startRightPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(0.3f, 5f);
        int trueOrFalse = Random.Range(0, 2);
        if (trueOrFalse == 0) startRightPos = true; else startRightPos = false;

        if (startRightPos)
        {
            transform.position = new Vector3(9,-0.5f,Random.Range(-7f, 12f));
        }else
        {
            transform.position = new Vector3(-9, -0.5f, Random.Range(-7f, 12f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startRightPos)
        {
            rb.velocity = Vector3.right * -speed;
        }else
        {
            rb.velocity = Vector3.right * speed;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (startRightPos)
        {
            if (collision.gameObject.tag == "LeftWall")
            {
                EnemyManager.instance.amountOfEnemiesToMake--;
                Destroy(gameObject);
                print(EnemyManager.instance.amountOfEnemiesToMake);
            }
        }
        else
        {
            if (collision.gameObject.tag == "RightWall")
            {
                EnemyManager.instance.amountOfEnemiesToMake--;
                Destroy(gameObject);
                print(EnemyManager.instance.amountOfEnemiesToMake);
            }
        }
    }

}
