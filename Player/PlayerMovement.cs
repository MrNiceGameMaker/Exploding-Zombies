using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    float xDir;
    float zDir;
    [SerializeField] float speed;
    Vector3 movement;

    [SerializeField]  float jumpForce;
    bool canJump;

    public static int hp;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = false;
        hp = 10;
        GameManager.instance.hpText.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        zDir = Input.GetAxis("Vertical") * speed;
        xDir = Input.GetAxis("Horizontal") * speed;
        movement = new Vector3(xDir,rb.velocity.y,zDir);   
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") canJump = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillPlayerBox")
        {
            hp--;
            GameManager.instance.hpText.text = "Life: " + hp.ToString();
        }
    }
}
