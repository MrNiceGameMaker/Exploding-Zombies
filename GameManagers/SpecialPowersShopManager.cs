using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SpecialPowersShopManager : MonoBehaviour
{
    int currentIndex;
    public FloatSO points;
    public SpecialPowersListSO specialPowersList;

    GameObject currentPowerUI;
    public Transform powerCamPos;
    public TMP_Text pointsTxt;
    public Button purchaseBtn;
    public Button upgradeBtn;
    public GameObject upgradeContainer;
    public List<Image> upgradeIndicators;
    public void LoadPowers(int powerIndex)
    {
        currentIndex = powerIndex;
        UpdateUI();
        SpecialPowersUI();
    }
    void UpdateUI()
    {
        pointsTxt.text = "Kash: " + points.value.ToString();
        var specialPower = specialPowersList.specialPowersList[currentIndex];
        purchaseBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.RemoveAllListeners();

        if (specialPower.isUnlocked)
        {
            purchaseBtn.gameObject.SetActive(false);
            upgradeContainer.SetActive(true);
            TMP_Text upgradeBtnText = upgradeBtn.GetComponentInChildren<TMP_Text>();
            for (int i = 0; i < upgradeIndicators.Count; i++)
            {
                if (i < specialPower.upgradedValue)
                {
                    upgradeIndicators[i].color = Color.green;
                }
                else
                {
                    upgradeIndicators[i].color = Color.gray;
                }
            }
            if (specialPower.upgradedValue >= 10)
            {
                upgradeBtn.interactable = false;
                upgradeBtnText.text = "Max";
            }
            else
            {
                upgradeBtnText.text = $"Upgrade ({specialPower.upgradePrice} Kash)";
                if (points.value >= specialPower.upgradePrice)
                {
                    upgradeBtn.interactable = true;
                    upgradeBtn.onClick.AddListener(UpgradePower);
                }
                else
                {
                    upgradeBtn.interactable = false;
                }
            }
        }
        else
        {
            purchaseBtn.gameObject.SetActive(true);
            upgradeContainer.SetActive(false);
            purchaseBtn.GetComponentInChildren<TMP_Text>().text = specialPower.purchasePrice + " Kash";
            purchaseBtn.onClick.AddListener(PurchasePower);

            if (points.value >= specialPower.purchasePrice)
            {
                purchaseBtn.interactable = true;
            }
            else
            {
                purchaseBtn.interactable = false;
            }
        }
    }
    void SpecialPowersUI()
    {
        if (currentPowerUI != null)
        {
            Destroy(currentPowerUI);
            CancelInvoke("RotateCurrentPowerUI");
        }
        currentPowerUI = Instantiate(specialPowersList.specialPowersList[currentIndex].powerObject, powerCamPos);
        currentPowerUI.transform.localPosition = new Vector3(0, 0, 3);
        currentPowerUI.transform.localRotation = Quaternion.identity;

        SpecialPowerEngine engineScript = currentPowerUI.GetComponent<SpecialPowerEngine>();
        if (engineScript != null)
        {
            Destroy(engineScript);
        }
        InvokeRepeating("RotateCurrentPowerUI", 0f, 0.01f);
    }
    void RotateCurrentPowerUI()
    {
        if (currentPowerUI != null)
        {
            currentPowerUI.transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);
        }
        else
        {
            CancelInvoke("RotateCurrentPowerUI");
        }
    }
    void PurchasePower()
    {
        var specialPower = specialPowersList.specialPowersList[currentIndex];
        if (points.value >= specialPower.purchasePrice && !specialPower.isUnlocked)
        {
            points.value -= specialPower.purchasePrice;
            specialPower.isUnlocked = true;
            specialPower.upgradePrice = 50;
            SpecialPowersSaveLoadManager.instance.SaveSpecialPowerData();
            UpdateUI();
        }
    }
    void UpgradePower()
    {
        var specialPower = specialPowersList.specialPowersList[currentIndex];
        if (points.value >= specialPower.upgradePrice)
        {
            points.value -= specialPower.upgradePrice;
            specialPower.upgradedValue++;
            specialPower.upgradePrice += 50;
            SpecialPowersSaveLoadManager.instance.SaveSpecialPowerData();
            UpdateUI();
        }
    }
    public void FreeKash()
    {
        points.value += 50;
        UpdateUI();
    }
}

