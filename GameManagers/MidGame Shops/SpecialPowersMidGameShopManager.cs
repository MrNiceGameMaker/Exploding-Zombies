using Michsky.UI.Heat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialPowersMidGameShopManager : MonoBehaviour
{
    int currentPowerIndex;
    public FloatSO points;
    public SpecialPowersListSO powersList;
    public GameObject[] powersShopItems;

    public void LoadPowerItem(int powerIndex)
    {
        currentPowerIndex = powerIndex;
        UpdatePowersUI();
    }

    public void UpdatePowersUI()
    {
        int startPowerIndex = currentPowerIndex * powersShopItems.Length;
        for (int i = 0; i < powersShopItems.Length; i++)
        {
            int powerIndex = startPowerIndex + i;
            if (powerIndex < powersList.specialPowersList.Count)
            {
                SpecialPowerSO currentPower = powersList.specialPowersList[powerIndex];
                GameObject powerShopItem = powersShopItems[i];
                ShopButtonManager buttonManager = powerShopItem.GetComponent<ShopButtonManager>();
                RawImage powerIcon = powerShopItem.transform.Find("Content/Item Details/Icon").GetComponent<RawImage>();
                Button purchaseButton = powerShopItem.transform.Find("PurchaseBtn").GetComponent<Button>();

                if (buttonManager != null)
                {
                    buttonManager.SetText(currentPower.powerName);
                    buttonManager.buttonDescription = currentPower.isUnlocked ? currentPower.powerName + " already purchased" : "Get the " + currentPower.powerName + " for " + currentPower.purchasePrice + " Kash!";
                    buttonManager.SetPrice(currentPower.purchasePrice.ToString());
                    buttonManager.SetIcon(currentPower.powerIcon);

                    if (currentPower.isUnlocked)
                    {
                        buttonManager.SetState(ShopButtonManager.State.Purchased);
                        buttonManager.SetInteractable(false);
                        purchaseButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        buttonManager.SetState(ShopButtonManager.State.Default);
                        bool canPurchase = points.value >= currentPower.purchasePrice;
                        buttonManager.SetInteractable(canPurchase);
                        purchaseButton.interactable = canPurchase;
                        purchaseButton.gameObject.SetActive(true);

                        buttonManager.onClick.RemoveAllListeners();
                        if (canPurchase)
                        {
                            buttonManager.onClick.AddListener(() => PurchasePower(currentPower));
                        }
                    }
                }
                powerIcon.texture = currentPower.powerIcon.texture;
                powerShopItem.SetActive(true);
            }
            else
            {
                powersShopItems[i].SetActive(false);
            }
        }
    }
    void PurchasePower(SpecialPowerSO power)
    {
        if (points.value >= power.purchasePrice)
        {
            points.value -= power.purchasePrice;
            power.isUnlocked = true;
            UpdatePowersUI();
            FindObjectOfType<WeaponsMidGameShopManager>().UpdateWeaponsUI();
            Debug.Log("Power purchased: " + power.powerName);
            SpecialPowersSaveLoadManager.instance.SaveSpecialPowerData();
        }
    }

}
