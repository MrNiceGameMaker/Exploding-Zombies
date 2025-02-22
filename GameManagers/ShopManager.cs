using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    int currentIndex;
    public WeaponsListSO weaponsListSO;
    public FloatSO points;

    GameObject currentWeaponAnim;
    public RawImage weaponAnimPreview;
    public Transform weaponCamPos;
    public TMP_Text pointsTxt;
    public Button purchaseBtn;
    public TMP_Text purchaseBtnTxt;

    public GameObject upgradeWeapon;
    public GameObject upgradeMissileLauncher;
    public Button[] upgradeBtns;
    public TMP_Text[] upgradeBtnsTxts;
    public Image[] upgradeDamageIndicators;
    public Image[] upgradeClipSizeIndicators;
    public Image[] upgradeReloadingTimeIndicators;
    public Image[] upgradeTimeBetweenShotsIndicators;
    public Image[] upgradeExplosionDamageIndicators;
    private void OnEnable()
    {
        WeaponsAnimUI();
    }
    private void OnDisable()
    {
        if (currentWeaponAnim != null)
        {
            Destroy(currentWeaponAnim);
            currentWeaponAnim = null;
        }
    }
    public void LoadItems(int weaponIndex)
    {
        pointsTxt.text = "Kash: " + points.value.ToString();
        currentIndex = weaponIndex;
        CheckPurchaseable();
        WeaponsAnimUI();
    }
    void WeaponsAnimUI()
    {
        if (currentWeaponAnim != null)
        {
            Destroy(currentWeaponAnim);
        }
        currentWeaponAnim = Instantiate(weaponsListSO.weaponsList[currentIndex].weaponObject, weaponCamPos);
        DisableProblematicScripts(currentWeaponAnim);

        currentWeaponAnim.gameObject.SetActive(true);
        
        Animator weaponAnimator = currentWeaponAnim.GetComponent<Animator>();
        weaponAnimator.SetLayerWeight(weaponAnimator.GetLayerIndex("ShopLayer"), 1);
        weaponAnimator.SetLayerWeight(weaponAnimator.GetLayerIndex("Base Layer"), 0);
    }
    void DisableProblematicScripts(GameObject weapon)
    {
        string[] problematicScripts = { "MuzzleFlashEngine","MissileLuncherEngine", "RotateMinigun", "Flamethrower" };

        foreach (Transform child in weapon.GetComponentsInChildren<Transform>())
        {
            foreach (string scriptName in problematicScripts)
            {
                var script = child.GetComponent(scriptName);
                if (script != null)
                {
                    MonoBehaviour monoBehaviourScript = script as MonoBehaviour;
                    if (monoBehaviourScript != null)
                    {
                        monoBehaviourScript.enabled = false;
                    }
                }
            }
        }
    }
    void CheckPurchaseable()
    {
        if (points.value >= weaponsListSO.weaponsList[currentIndex].purchasePrice && !weaponsListSO.weaponsList[currentIndex].isUnlocked)
        {
            upgradeWeapon.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
            purchaseBtnTxt.text = weaponsListSO.weaponsList[currentIndex].purchasePrice.ToString() + " Kash";
            purchaseBtn.interactable = true;
            upgradeMissileLauncher.gameObject.SetActive(false);
            weaponAnimPreview.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            weaponAnimPreview.rectTransform.anchoredPosition= new Vector2(0, -5);
        }
        else if (weaponsListSO.weaponsList[currentIndex].isUnlocked)
        {
            purchaseBtn.gameObject.SetActive(false);
            upgradeWeapon.SetActive(true);
            UpdateUpgradeWeapon();
            weaponAnimPreview.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            weaponAnimPreview.rectTransform.anchoredPosition = new Vector2(114.95f, 40);

        }
        else
        {
            upgradeWeapon.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
            purchaseBtnTxt.text = weaponsListSO.weaponsList[currentIndex].purchasePrice.ToString() + " Kash";
            purchaseBtn.interactable = false;
            upgradeMissileLauncher.gameObject.SetActive(false);
            weaponAnimPreview.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            weaponAnimPreview.rectTransform.anchoredPosition = new Vector2(0, -5);
        }

    }
    public void FreeKash()
    {
        points.value += 50;
        pointsTxt.text = "Kash: " + points.value.ToString();
        CheckPurchaseable();
    }
    public void PurchaseWeapon()
    {
        weaponsListSO.weaponsList[currentIndex].isUnlocked = true;
        points.value = points.value - weaponsListSO.weaponsList[currentIndex].purchasePrice;
        pointsTxt.text = "Kash: " + points.value.ToString();
        CheckPurchaseable();
        WeaponSaveLoadManager.instance.SaveWeaponData();
    }
    public void UpgradeDamage()
    {
        weaponsListSO.weaponsList[currentIndex].damage += 5;
        points.value -= weaponsListSO.weaponsList[currentIndex].upgradeDamagePrice;
        weaponsListSO.weaponsList[currentIndex].upgradeDamagePrice += 50;
        weaponsListSO.weaponsList[currentIndex].upgradeDamageCount += 1;
        pointsTxt.text = "Kash: " + points.value.ToString();
        UpdateUpgradeWeapon();
        WeaponSaveLoadManager.instance.SaveWeaponData();
    }
    public void UpgradeClipSize()
    {
        weaponsListSO.weaponsList[currentIndex].clipSize += 1;
        points.value -= weaponsListSO.weaponsList[currentIndex].upgradeClipSizePrice;
        weaponsListSO.weaponsList[currentIndex].upgradeClipSizePrice += 50;
        weaponsListSO.weaponsList[currentIndex].upgradeClipSizeCount += 1;
        pointsTxt.text = "Kash: " + points.value.ToString();
        UpdateUpgradeWeapon();
        WeaponSaveLoadManager.instance.SaveWeaponData();
    }
    public void UpgradeReloadingTime()
    {
        weaponsListSO.weaponsList[currentIndex].reloadingTime -= weaponsListSO.weaponsList[currentIndex].reloadingTime * 0.1f;
        points.value -= weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimePrice;
        weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimePrice += 50;
        weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimeCount += 1;
        pointsTxt.text = "Kash: " + points.value.ToString();
        UpdateUpgradeWeapon();
        WeaponSaveLoadManager.instance.SaveWeaponData();
    }
    public void UpgradeTimeBetweenShots()
    {
        weaponsListSO.weaponsList[currentIndex].timeBetweenShots -= weaponsListSO.weaponsList[currentIndex].timeBetweenShots * 0.1f;
        points.value -= weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsPrice;
        weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsPrice += 50;
        weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsCount += 1;
        pointsTxt.text = "Kash: " + points.value.ToString();
        UpdateUpgradeWeapon();
        WeaponSaveLoadManager.instance.SaveWeaponData();
    }
    public void UpgradeExplosionDamage()
    {
        weaponsListSO.weaponsList[currentIndex].explosionDamage += 5;
        points.value -= weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamagePrice;
        weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamagePrice += 50;
        weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamageCount += 1;
        pointsTxt.text = "Kash: " + points.value.ToString();
        UpdateUpgradeWeapon();
        WeaponSaveLoadManager.instance.SaveWeaponData();
    }
    void UpdateUpgradeWeapon()
    {
        bool isDamageMaxed = weaponsListSO.weaponsList[currentIndex].upgradeDamageCount >= 7;
        bool isClipSizeMaxed = weaponsListSO.weaponsList[currentIndex].upgradeClipSizeCount >= 7;
        bool isReloadingTimeMaxed = weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimeCount >= 7;
        bool isTimeBetweenShotsMaxed = weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsCount >= 7;
        bool isExplosionDamageMaxed = currentIndex == 5 && weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamageCount >= 7;

        upgradeBtns[0].interactable = !isDamageMaxed && points.value >= weaponsListSO.weaponsList[currentIndex].upgradeDamagePrice;
        upgradeBtns[1].interactable = !isClipSizeMaxed && points.value >= weaponsListSO.weaponsList[currentIndex].upgradeClipSizePrice;
        upgradeBtns[2].interactable = !isReloadingTimeMaxed && points.value >= weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimePrice && weaponsListSO.weaponsList[currentIndex].reloadingTime > 0;
        upgradeBtns[3].interactable = !isTimeBetweenShotsMaxed && points.value >= weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsPrice && weaponsListSO.weaponsList[currentIndex].timeBetweenShots > 0;

        upgradeBtnsTxts[0].text = isDamageMaxed ? "MAX" : weaponsListSO.weaponsList[currentIndex].upgradeDamagePrice + " Kash";
        upgradeBtnsTxts[1].text = isClipSizeMaxed ? "MAX" : weaponsListSO.weaponsList[currentIndex].upgradeClipSizePrice + " Kash";
        upgradeBtnsTxts[2].text = isReloadingTimeMaxed ? "MAX" : weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimePrice + " Kash";
        upgradeBtnsTxts[3].text = isTimeBetweenShotsMaxed ? "MAX" : weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsPrice + " Kash";

        UpdateUpgradeIndicators(upgradeDamageIndicators, weaponsListSO.weaponsList[currentIndex].upgradeDamageCount);
        UpdateUpgradeIndicators(upgradeClipSizeIndicators, weaponsListSO.weaponsList[currentIndex].upgradeClipSizeCount);
        UpdateUpgradeIndicators(upgradeReloadingTimeIndicators, weaponsListSO.weaponsList[currentIndex].upgradeReloadingTimeCount);
        UpdateUpgradeIndicators(upgradeTimeBetweenShotsIndicators, weaponsListSO.weaponsList[currentIndex].upgradeTimeBetweenShotsCount);

        if (currentIndex == 5)
        {
            upgradeMissileLauncher.gameObject.SetActive(true);
            upgradeBtns[4].interactable = !isExplosionDamageMaxed && points.value >= weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamagePrice;
            upgradeBtnsTxts[4].text = isExplosionDamageMaxed ? "MAX" : weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamagePrice + " Kash";
            UpdateUpgradeIndicators(upgradeExplosionDamageIndicators, weaponsListSO.weaponsList[currentIndex].upgradeExplosionDamageCount);
        }
        else
        {
            upgradeMissileLauncher.gameObject.SetActive(false);
        }
    }
    void UpdateUpgradeIndicators(Image[] indicators, int count)
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            if (i < count)
            {
                indicators[i].color = Color.green;
            }
            else
            {
                indicators[i].color = Color.gray;
            }
        }
    }
}
