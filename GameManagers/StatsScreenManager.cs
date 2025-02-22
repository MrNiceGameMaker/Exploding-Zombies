using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreenManager : MonoBehaviour
{
    [SerializeField] IntSO level;
    [SerializeField] IntSO currentZone;
    [SerializeField] BoolSO isInBossFight;

    [SerializeField] TMP_Text currentZoneText;

    public static StatsScreenManager instance;
    [SerializeField] TMP_Text levelPassedText;
    [SerializeField] TMP_Text timeText;
    //[SerializeField] Button nextLevelButton;
    [SerializeField] List<Sprite> backgroundImages = new List<Sprite>();
    [SerializeField] Image currentBackgroundImage;
    private void Awake()
    {
        instance = this;
    }
    public void ShowCurrnetLevel()
    {
        if (!isInBossFight.value)
        {
            if (level.value == 1)
            {
                currentZoneText.text = "ZONE " + (currentZone.value) + "CLEARED!";
                levelPassedText.text = "Starting new zone";
            }
            else levelPassedText.text = "You cleared: " + (level.value-1) + " waves";
        }
        else
        {
            currentZoneText.text = "ZONE " + (currentZone.value + 1);
            levelPassedText.text = "BOSS WAVE IS COMING!";
        }
        
        //nextLevelButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Next Wave";
        timeText.text = "Time Since Start: "+ Time.time.ToString("N0") + " sec";
        if(currentZone.value < backgroundImages.Count)
        {
            currentBackgroundImage.sprite = backgroundImages[currentZone.value];
        }else
        {
            currentBackgroundImage.sprite = backgroundImages[Random.Range(0, backgroundImages.Count)];  
        }
    }
}
