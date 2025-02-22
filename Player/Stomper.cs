using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float jumpForce;

    [SerializeField] GameObject expPS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject temp = Instantiate(expPS, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            playerRb.AddForce(0, jumpForce, 0);
            EnemyManager.instance.amountOfEnemiesToMake--;
            EnemyManager.instance.amountOfEnemiesKilled++;
            GameManager.instance.amountOfEnemiesKilledText.text = "Enemies Killed: "+ EnemyManager.instance.amountOfEnemiesKilled.ToString();
        }

    }
    public static void DestroyEnemy()
    {

    }
}
