using UnityEngine;
using UnityEngine.Events;

public class ActivateSpecialPower : MonoBehaviour
{
    public static void SetSpecialPower(int powerID)
    {
        SpecialPowersManager.Instance.playerPowersManager = (PlayerPowersManager)powerID;
        SpecialPowersObjectUI.instance.UpdateSpecialPowerUI(powerID);
    }
    public void ActivatePowerPlayer()
    {
        StartCoroutine(SpecialPowersManager.Instance.ActivatePower());
        SpecialPowersManager.Instance.playerPowersManager = PlayerPowersManager.None;
        SpecialPowersObjectUI.instance.UpdateSpecialPowerUI(5);
    }
}
