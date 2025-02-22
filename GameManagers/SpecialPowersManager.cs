using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialPowersManager : MonoBehaviour
{
    public static SpecialPowersManager Instance { get; private set; }
    public PlayerPowersManager playerPowersManager;
    public SpecialPowersListSO specialPowersListSO;

    [SerializeField] Rigidbody playerRB;
    [SerializeField] Transform playerPos;
    [SerializeField] public GameObject expPS;
    [SerializeField] public GameObject BlackHawk;
    [SerializeField] public GameObject explosiveBarrelPrefab;

    [SerializeField] float freezeTime;
    [SerializeField] float explosionSize;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        playerPowersManager = PlayerPowersManager.None;
    }

    public IEnumerator ActivatePower()
    {
        int powerIndex = (int)playerPowersManager;
        var specialPower = specialPowersListSO.specialPowersList[powerIndex];
        if (playerPowersManager == PlayerPowersManager.Explosion)
        {
            SpawnExplosiveBarrel();
        }
        else if (playerPowersManager == PlayerPowersManager.SlowDownTime)
        {
            freezeTime += specialPower.upgradedValue;
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(freezeTime);
            Time.timeScale = 1f;
            playerPowersManager = PlayerPowersManager.None;
        }
        else if (playerPowersManager == PlayerPowersManager.AddLife)
        {
            int additionalLife = 1 + (int)specialPower.upgradedValue;
            PlayerMovementUI.hp++;
            GameManager.instance.hpText.text = "Life: " + PlayerMovementUI.hp.ToString();
        }
        else if (playerPowersManager == PlayerPowersManager.Coins)
        {
            // הוסף כאן את הקוד של כוח הכסף
        }
        else if (playerPowersManager == PlayerPowersManager.AirStrike)
        {
            BlackHawk.SetActive(true);
            explosionSize += specialPower.upgradedValue;
            StartCoroutine(MakeExplosion(50, 2.5f));
        }
    }
    void SpawnExplosiveBarrel()
    {
        float randomX = Random.Range(-8.6f, 7.89f);
        float randomZ = Random.Range(-11.68f, 0f);

        Vector3 spawnPosition = new Vector3(randomX, 10f, randomZ);
        Instantiate(explosiveBarrelPrefab, spawnPosition, Quaternion.identity);
    }

    public IEnumerator MakeExplosion(float explosionSize, float timeBeforeStartExplosion)
    {
        yield return new WaitForSeconds(timeBeforeStartExplosion);
        Vector3 explosionPos = playerPos.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionSize);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy" && hit != null)
            {
                float timeTillDeath = Random.Range(0, 0.05f);
                yield return new WaitForSeconds(timeTillDeath);
                GameObject temp = CreateExplosionPool.SharedInstance.GetPooledObject(Random.Range(0, CreateExplosionPool.SharedInstance.pooledObjects.Count));
                StartCoroutine(CreateExplosion(temp, hit));
                hit.gameObject.SetActive(false);
                EnemyManager.instance.amountOfEnemiesKilled++;
                GameManager.instance.amountOfEnemiesKilledText.text = "Enemies Killed: " + EnemyManager.instance.amountOfEnemiesKilled.ToString();
            }
        }
    }

    public IEnumerator CreateExplosion(GameObject temp, Collider hit)
    {
        temp.transform.position = hit.transform.position;
        temp.transform.rotation = hit.transform.rotation;
        temp.SetActive(true);
        yield return new WaitForSeconds(1);
        temp.SetActive(false);
    }
}
