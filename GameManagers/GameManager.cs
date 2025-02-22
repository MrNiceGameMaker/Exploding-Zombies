using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text hpText;
    public TMP_Text amountOfEnemiesKilledText;
    public TMP_Text timeToNextWaveText;

    [SerializeField]  IntSO level;
    [SerializeField]  IntSO currentZone;
    [SerializeField] UnityEvent changeZone;
    //public int level;
    public int timeToNextWave;

    bool startBossNextLevel;
    [SerializeField] UnityEvent onBossStart;
    [SerializeField] GameObject statsScreen;

    [SerializeField] BoolSO isInBossFight;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    private void Start()
    {
        isInBossFight.value = false;
        statsScreen.SetActive(false);
        startBossNextLevel = false;
        timeToNextWave = 30;
        StartCoroutine(LevelUp());
        StartCoroutine(CreatePeople());
    }
    IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(1);
        timeToNextWave--;
        if (timeToNextWave <= 0)
        {
            if(startBossNextLevel)
            {
                isInBossFight.value = true;
                onBossStart.Invoke();
                StartNextLevelStatsScreen();
            }
            else
            {
                StartNextLevelStatsScreen();
            }
        }
        timeToNextWaveText.text = "Wave " + (level.value).ToString() + ". Next Wave In " + timeToNextWave;
        StartCoroutine(LevelUp());
    }
    IEnumerator CreatePeople()
    {
        int maxTimeToCreatePeople = 45 - level.value;
        int maxAmountOfPeopleToMake = 2 + level.value;
        yield return new WaitForSeconds(Random.Range(5, maxTimeToCreatePeople));
        GameObject temp = CreatePeoplePool.SharedInstance.GetPooledObject
                          (Random.Range(0, CreatePeoplePool.SharedInstance.objectToPool.Length));
        temp.SetActive(true);
        temp.transform.position = new Vector3(Random.Range(-8f, 8f), -0.2f, 18);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newRotationY = currentRotation.y + 180f;
        temp.transform.rotation = Quaternion.Euler(currentRotation.x, newRotationY, currentRotation.z);
        StartCoroutine(CreatePeople());
    }
    void StartNextLevelStatsScreen()
    {
        if (isInBossFight.value)
        {
            timeToNextWave = 1800;
        }else
        {
            timeToNextWave = 30;
            level.value++;
            if (level.value % 10 == 0) startBossNextLevel = true;
        }
        Time.timeScale = 0;
        StatsScreenManager.instance.ShowCurrnetLevel();
        statsScreen.SetActive(true);

    }
    public void StartStatsScreen()
    {
        Time.timeScale = 0;
        StatsScreenManager.instance.ShowCurrnetLevel();
        statsScreen.SetActive(true);
        startBossNextLevel = false;
        isInBossFight.value = false;
        level.value = 1;
        currentZone.value++;
        changeZone.Invoke();
    }
    public void NextLevelButton()
    {
        Time.timeScale = 1;
        statsScreen.SetActive(false);
        if (!isInBossFight.value)
        {
            timeToNextWave = 30;
            EnemyManager.instance.CreateEnemiesWave();
        }
    }
}
