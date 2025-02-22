using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Weapons")]

    public WeaponsListSO weaponsList;

    [SerializeField] TMP_Text currentWeaponText;
    [SerializeField] TMP_Text amountOfBulltesText;

    [Header("Enemies")]
    [SerializeField] GameObject timeTillNextWaveText;
    [SerializeField] GameObject bossFightText;

    [Header("Settings")]
    [SerializeField] GameObject settingsMenu;

    [Header("Zones")]
    [SerializeField] IntSO currnetZone;
    [SerializeField] TMP_Text currentZoneText;

    [Header("Points")]
    [SerializeField] TMP_Text pointsText;
    [SerializeField] FloatSO points;

    [SerializeField] GameObject hitStreak;
    [SerializeField] TMP_Text killstreakMultiplierText;
    [SerializeField] IntSO killstreakMultiplier;

    [SerializeField] GameObject pointToAdd;
    [SerializeField] TMP_Text pointsToAddText;

    private void Start()
    {
        currentWeaponText.text = weaponsList.weaponsList[weaponsList.index].name;
        amountOfBulltesText.text = weaponsList.weaponsList[weaponsList.index].bulletsInClip + "/" + weaponsList.weaponsList[weaponsList.index].clipSize;
        bossFightText.SetActive(false);
        currentZoneText.text = "Zone: " + 1 ;

    }
    private void Update()
    {
        if (!Application.isFocused)
        {
            OpenCloseSettings();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            OpenCloseSettings();
        }
    }
    public void ShowCurrnetWeapon()
    {
        currentWeaponText.text = weaponsList.weaponsList[weaponsList.index].name;
    }

    public void ShowAmountOfBullets()
    {
        amountOfBulltesText.text = weaponsList.weaponsList[weaponsList.index].bulletsInClip + "/" + weaponsList.weaponsList[weaponsList.index].clipSize;
    }
    public void InBossFight()
    {
        timeTillNextWaveText.SetActive(false);
        bossFightText.SetActive(true);
    }
    public void AfterBossFight()
    {
        timeTillNextWaveText.SetActive(true);
        bossFightText.SetActive(false);
    }

    public void OpenCloseSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        if(settingsMenu.activeSelf)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 1;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeZoneText()
    {
        currentZoneText.text = "Zone: " + (currnetZone.value+1);
    }

    public void UpdatePointsText()
    {
        pointsText.text = "Points: " + points.value;
    }
    public void AddPointsPerKillText(float pointsToAdd, Vector3 enemyPos)
    {
        pointToAdd.SetActive(true);
        pointsToAddText.text = "+" + pointsToAdd;
        StartCoroutine(DeactivateText(pointToAdd, 2));

    }
    public void UpdateHitSpreeText()
    {
        killstreakMultiplierText.text = "Hit Spree: " + killstreakMultiplier.value;
    }
    IEnumerator DeactivateText(GameObject textObj, float timeToDeactivate)
    {
        yield return new WaitForSeconds(timeToDeactivate);
        textObj.SetActive(false);
    }
}
