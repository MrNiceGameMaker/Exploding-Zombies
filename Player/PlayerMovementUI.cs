using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementUI : MonoBehaviour
{
    Rigidbody rb;


    [SerializeField] float speed;
    //Vector3 movement;
    [SerializeField] Vector3SO movement;
    [SerializeField] float jumpForce;
    bool canJump;

    [SerializeField] InputActionReference moveActionJoystick;
    [SerializeField] GameObject expPS;


    public static int hp;
    public static bool inGodMode;

    float zStartPos;

    Vector2 moveDir;
    public float acceleration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = false;
        hp = 10;
        GameManager.instance.hpText.text = "Life: " + hp.ToString();
        zStartPos = transform.position.z;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        
        moveDir = moveActionJoystick.action.ReadValue<Vector2>();
        if ((transform.position.x > -8.7f && moveDir.x < 0) ||
            (transform.position.x < 8.7f && moveDir.x > 0))
        {
           // movement = new Vector3(moveDir.x * speed, rb.velocity.y, zStartPos);
            float targetVelocity = moveDir.x * speed;
            float accelerationStep = Mathf.MoveTowards(rb.velocity.x, targetVelocity, acceleration * Time.deltaTime);

            // Set the new velocity
            movement.value = new Vector3(accelerationStep, 0f, zStartPos);
        }
        else
        {
            movement.value = Vector3.zero;
        }
        rb.velocity = movement.value;
        if (transform.position.x < -8.9f) transform.position = new Vector3(-8.7f, transform.position.y, transform.position.z);
        else if (transform.position.x > 8.9f) transform.position = new Vector3(8.7f, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inGodMode)
            {
            GameObject temp = Instantiate(SpecialPowersManager.Instance.expPS, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);
            Destroy(temp, 1);
            //EnemyManager.instance.amountOfEnemiesToMake--;
            EnemyManager.instance.amountOfEnemiesKilled++;
            GameManager.instance.amountOfEnemiesKilledText.text = "Enemies Killed: " + EnemyManager.instance.amountOfEnemiesKilled.ToString();
            }
    }

}
