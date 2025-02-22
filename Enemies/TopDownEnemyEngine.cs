using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownEnemyEngine : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public ColorsEnums enemyColorState;
    public int enemyHP;
    public int maxEnemyHP;
    public int listID;
    public bool firstDisable;
    [SerializeField] IntSO currentZone;

    [SerializeField] Slider healthBarSlider;

    bool wasKilled;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {
        //                              makes better enemies go faster
        speed = Random.Range(0.3f, 2f+(enemyHP*0.2f));

        if (currentZone.value > listID)
        {
            enemyHP = Random.Range(listID+1, currentZone.value+1);
        }else
        {
            enemyHP = listID + 1;
        }
        maxEnemyHP = enemyHP;
        healthBarSlider.value = 1;

        wasKilled = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.back * speed;
        if (transform.position.z < -21)
        {
            PlayerMovementUI.hp--;
            GameManager.instance.hpText.text = "Life: " + PlayerMovementUI.hp.ToString();
            wasKilled = false;
            gameObject.SetActive(false);
 
        }else if (transform.position.z > 21)
        {
            wasKilled = false;
            gameObject.SetActive(false);
        }
    }
    public void updateHealthBar(int damage)
    {
        enemyHP -= damage;
        healthBarSlider.value = (float)enemyHP/ (float)maxEnemyHP ;

    }
/*    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "BackWall")
        {
            //EnemyManager.instance.amountOfEnemiesToMake--;
            // Destroy(gameObject);
            gameObject.SetActive(false);
            PlayerMovementUI.hp--;
            GameManager.instance.hpText.text = "Life: " + PlayerMovementUI.hp.ToString();
        }
    }*/
    private void OnDisable()
    {
        if (!firstDisable)
        {
            EnemyManager.instance.UpdateEnemyKilled(transform.position, listID, wasKilled);
        }
        firstDisable = false;

    }

}
