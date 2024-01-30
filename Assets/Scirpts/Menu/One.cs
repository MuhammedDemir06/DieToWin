using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    [SerializeField] private PurchaseSystem systemPurchase;
    public static int SaveCharacterAsset;
    private void Update()
    {
        if(!systemPurchase.IsPurchase)
            PurchaseSystem.BanditIsSelected = true;

        if(PurchaseSystem.BanditIsSelected)
        {
            print("bandit selected");
            SaveCharacterAsset = 1;
            PlayerPrefs.SetInt("SaveCharacterAsset", SaveCharacterAsset);
        }
        if(PurchaseSystem.HeavyIsSelected)
        {
            print("heavySelected");
            SaveCharacterAsset = 2;
            PlayerPrefs.SetInt("SaveCharacterAsset", SaveCharacterAsset);
        }
    }
}
