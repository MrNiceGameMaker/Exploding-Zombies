using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    //[SerializeField] public GameObject[] enemies = new GameObject[6];

    [SerializeField] IntSO level;
    [SerializeField] IntSO currentZone;

    [SerializeField] ChanceToMakeHarderEnemySO chanceToMakeHarderEnemy;

    public int amountOfEnemiesToMake;
    public int amountOfEnemiesKilled;

    public ColorsEnums colorsEnmus;

    public int minAmountOfEnemiesInWave;
    public int maxAmountOfEnemiesInWave;
    [SerializeField] IntSO amountOfEnemiesLeftInWave;
    [SerializeField] BoolSO isInBossFight;

    [SerializeField] UnityEvent stopBossFigth;
    [SerializeField] UnityEvent<Vector3,int> enemyKilled;

    [SerializeField] IntSO hitStreakMultiplier;
    [SerializeField] UnityEvent onZombiePassingPlayer;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        amountOfEnemiesLeftInWave.value = 0;
        level.value = 1;
        CreateEnemiesWave();
    }

    public void CreateEnemiesWave()
    {
        minAmountOfEnemiesInWave = 0 + level.value; 
        maxAmountOfEnemiesInWave = 3 + level.value;
        amountOfEnemiesToMake = Random.Range(minAmountOfEnemiesInWave, maxAmountOfEnemiesInWave);
        amountOfEnemiesLeftInWave.value += amountOfEnemiesToMake;

        for (int i = 0; i < amountOfEnemiesToMake; i++)
        {
            CreateEnemy();
        }
    }
    public void CreateEnemy()
    {
        int randomNum = Random.Range(0, 100);
        int enemyIndex = 0;
        for(int i = 1;i < CreateEnemyPool.SharedInstance.objectToPool.Length; i++)
        {
            if (randomNum < (chanceToMakeHarderEnemy.value - (level.value+(currentZone.value*2))))
            {              
                break;
            }
            else randomNum = Random.Range(0, 100);
            enemyIndex = i;
        }
        GameObject temp = CreateEnemyPool.SharedInstance.GetPooledObject(enemyIndex);
        temp.SetActive(true);
        temp.transform.position = new Vector3(Random.Range(-8f, 8f), -0.2f, 18);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newRotationY = currentRotation.y + 180f;
        temp.transform.rotation = Quaternion.Euler(currentRotation.x, newRotationY, currentRotation.z);
    }

    public void UpdateEnemyKilled(Vector3 killedPos, int enemyLevel, bool wasKilled)
    {
        amountOfEnemiesLeftInWave.value--;
        if (wasKilled) enemyKilled.Invoke(killedPos, enemyLevel);
        else
        {
            hitStreakMultiplier.value = 0;
            onZombiePassingPlayer.Invoke();
        }

        if (amountOfEnemiesLeftInWave.value <= 0)
        {
            if (isInBossFight.value)
            {
                Invoke(nameof(StopBossFight), 3);
            }
            else
            {
                GameManager.instance.timeToNextWave = 3;
            }
        }
    }

    void StopBossFight()
    {
        stopBossFigth.Invoke();
    }
}
