using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject settings;
    [SerializeField] GameObject weaponsShop;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject specialPowersShop;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
    public void OpenShop()
    {
        menu.SetActive(false);
        weaponsShop.SetActive(true);
    }
    public void OpenPowersShop()
    {
        menu.SetActive(false);
        specialPowersShop.SetActive(true);
    }
    public void OpenMenu()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        specialPowersShop.SetActive(false);
        weaponsShop.SetActive(false);
    }
}
