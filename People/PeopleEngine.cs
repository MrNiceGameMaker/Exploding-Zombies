using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleEngine : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {
        speed = Random.Range(1f, 3.5f);

    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.back * speed;
        if (transform.position.z < -24)
        {
            PlayerMovementUI.hp++;
            GameManager.instance.hpText.text = "Life: " + PlayerMovementUI.hp.ToString();
            gameObject.SetActive(false);
            //points+=10;

        }
    }


}
