using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialPowersObjectUI : MonoBehaviour
{
    public static SpecialPowersObjectUI instance;
    [SerializeField] List<Sprite> specialPowersSprites;
    [SerializeField] Button ActionButton;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ActionButton.image.sprite = specialPowersSprites[5];
    }
    public void UpdateSpecialPowerUI(int powerID)
    {
        ActionButton.image.sprite = specialPowersSprites[powerID];
    }
}
