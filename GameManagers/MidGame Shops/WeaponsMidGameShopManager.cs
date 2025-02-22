using Michsky.UI.Heat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsMidGameShopManager : MonoBehaviour
{
    int currentWeaponIndex;

    public FloatSO points;
    public WeaponsListSO weaponsList;

    public TMP_Text pointsTxt;
    public GameObject[] weaponsShopItems;

    public void LoadWeaponItem(int weaponIndex)
    {
        currentWeaponIndex = weaponIndex;
        UpdateWeaponsUI();
    }

    public void UpdateWeaponsUI()
    {
        pointsTxt.text = "Kash: " + points.value.ToString();
        int startWeaponIndex = currentWeaponIndex * weaponsShopItems.Length;
        for (int i = 0; i < weaponsShopItems.Length; i++)
        {
            int weaponIndex = startWeaponIndex + i;
            if (weaponIndex < weaponsList.weaponsList.Count)
            {
                WeaponsSO currentWeapon = weaponsList.weaponsList[weaponIndex];
                GameObject weaponShopItem = weaponsShopItems[i];
                ShopButtonManager buttonManager = weaponShopItem.GetComponent<ShopButtonManager>();
                RawImage weaponIcon = weaponShopItem.transform.Find("Content/Item Details/Icon").GetComponent<RawImage>();
                Button purchaseButton = weaponShopItem.transform.Find("PurchaseBtn").GetComponent<Button>();

                if (buttonManager != null)
                {
                    buttonManager.SetText(currentWeapon.name);
                    buttonManager.buttonDescription = currentWeapon.isUnlocked ? currentWeapon.weaponName + " already purchased" : "Get the " + currentWeapon.weaponName + " for " + currentWeapon.purchasePrice + " Kash!";
                    buttonManager.SetPrice(currentWeapon.purchasePrice.ToString());
                    buttonManager.SetIcon(currentWeapon.weaponIcon);

                    if (currentWeapon.isUnlocked)
                    {
                        buttonManager.SetState(ShopButtonManager.State.Purchased);
                        buttonManager.SetInteractable(false);
                        purchaseButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        buttonManager.SetState(ShopButtonManager.State.Default);
                        bool canPurchase = points.value >= currentWeapon.purchasePrice;
                        buttonManager.SetInteractable(canPurchase);
                        purchaseButton.interactable = canPurchase;
                        purchaseButton.gameObject.SetActive(true);

                        buttonManager.onClick.RemoveAllListeners();
                        if (canPurchase)
                        {
                            buttonManager.onClick.AddListener(() => PurchaseWeapon(currentWeapon));
                        }
                    }
                }
                weaponIcon.texture = currentWeapon.weaponIcon.texture;
                weaponShopItem.SetActive(true);
            }
            else
            {
                weaponsShopItems[i].SetActive(false);
            }
        }
    }

    void PurchaseWeapon(WeaponsSO weapon)
    {
        if (points.value >= weapon.purchasePrice)
        {
            points.value -= weapon.purchasePrice;
            weapon.isUnlocked = true;
            UpdateWeaponsUI();
            FindObjectOfType<SpecialPowersMidGameShopManager>().UpdatePowersUI();
            Debug.Log("Weapon purchased: " + weapon.weaponName);
            WeaponSaveLoadManager.instance.SaveWeaponData();
        }
    }

    public void FreeKash()
    {
        points.value += 25;
        UpdateWeaponsUI();
        FindObjectOfType<SpecialPowersMidGameShopManager>().UpdatePowersUI();
    }
}
