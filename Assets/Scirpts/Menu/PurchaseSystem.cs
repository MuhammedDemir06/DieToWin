using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurchaseSystem : MonoBehaviour
{
    public static PurchaseSystem PurchaseInterface;
    [Tooltip("Not:price 0=heavyPrice")][SerializeField] private int price;
    [SerializeField] private MenuController controllerMenu;
    //text
    [SerializeField] private Text purchaseText;
    [SerializeField] private Image purchaseButton,coinImage,selectButton;
    [SerializeField] private int index;
    public bool IsPurchase;
    //Select
    [SerializeField] private Image[] character;
    [SerializeField] private Text selectTextBandit, selectTextHeavy;
    public static bool BanditIsSelected, HeavyIsSelected;
    //price
    [SerializeField] private Text priceText;
    //
    public static int characterSelectNum;
    private int characterIndexSelect;
    private void Start()
    {
        PurchaseInterface = this;
        //default 
        characterIndexSelect = PlayerPrefs.GetInt("CharacterSelect");
        index = PlayerPrefs.GetInt("IndexPurchase");
    }
    public void Data()
    {
        PlayerPrefs.DeleteAll();
    }
    private void Update()
    {
        priceText.text = price.ToString();
        //index 
        if(index>0)
        {
            IsPurchase = true;
        }
        if(IsPurchase==true)
        {
            purchaseButton.color = Color.green;
            coinImage.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            //purchase text
            if (LanguageControlPanel.TurkishLanguage == true)
            {
                purchaseText.text = "Mevcut";
            }
            if (LanguageControlPanel.EnglishLanguage == true)
            {
                purchaseText.text = "Available";
            }
            if (LanguageControlPanel.JapeneseLanguage == true)
            {
                purchaseText.text = "利用可能";
            }
        }
        if(IsPurchase==false)
        {
            coinImage.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            //purchase text
            if (LanguageControlPanel.TurkishLanguage == true)
            {
                purchaseText.text = "Satın al";
            }
            if (LanguageControlPanel.EnglishLanguage == true)
            {
                purchaseText.text = "Purchase";
            }
            if (LanguageControlPanel.JapeneseLanguage == true)
            {
                purchaseText.text = "購入";
            }
        }
        CharacterChooseController();
        SelectButtonData();
    }
    private void CharacterChooseController()
    {
        if(BanditIsSelected)
        {
            BanditIsSelected = true;
            HeavyIsSelected = false;
            character[0].color = Color.blue;
            character[1].color = Color.white;
            if (LanguageControlPanel.TurkishLanguage == true)
            {
                selectTextBandit.text = "Seçildi";
            }
            if (LanguageControlPanel.EnglishLanguage == true)
            {
                selectTextBandit.text = "Selected";
            }
            if (LanguageControlPanel.JapeneseLanguage == true)
            {
                selectTextBandit.text = "厳選された";
            }
        }
        else
        {
            if (LanguageControlPanel.TurkishLanguage == true)
            {
                selectTextBandit.text = "Seç";
            }
            if (LanguageControlPanel.EnglishLanguage == true)
            {
                selectTextBandit.text = "Select";
            }
            if (LanguageControlPanel.JapeneseLanguage == true)
            {
                selectTextBandit.text = "選択";
            }
           
           
        }
        if(HeavyIsSelected)
        {
            HeavyIsSelected = true;
            BanditIsSelected = false;
            character[1].color = Color.blue;
            character[0].color = Color.white;
            if (LanguageControlPanel.TurkishLanguage == true)
            {
                selectTextHeavy.text = "Seçildi";
            }
            if (LanguageControlPanel.EnglishLanguage == true)
            {
                selectTextHeavy.text = "Selected";
            }
            if (LanguageControlPanel.JapeneseLanguage == true)
            {
                selectTextHeavy.text = "厳選された";
            }
        }
        else
        {
            if (LanguageControlPanel.TurkishLanguage == true)
            {
                selectTextHeavy.text = "Seç";
            }
            if (LanguageControlPanel.EnglishLanguage == true)
            {
                selectTextHeavy.text = "Select";
            }
            if (LanguageControlPanel.JapeneseLanguage == true)
            {
                selectTextHeavy.text = "選択";
            }  
        }
    }
    public void PurchaseButton()
    {
        if(controllerMenu.CoinNumber>=price)
        {
            if(index<3)
            {
                index++;
            }
            if(!IsPurchase)
            {
               
                PlayerPrefs.SetInt("IndexPurchase", index);
                print("purchase succesful");
                controllerMenu.CoinNumber = controllerMenu.CoinNumber - price;
                //   purchaseText.gameObject.SetActive(false);
            }
        }
        else
            print("purchase unsuccesful");
    }
    private void SelectButtonData()
    {
        switch (characterIndexSelect)
        {
            case 0:
                PlayerPrefs.SetInt("CharacterSelect", characterIndexSelect);
                characterSelectNum = 1;
                BanditIsSelected = true;
                HeavyIsSelected = false;
                character[0].color = Color.blue;
                character[1].color = Color.white;
                break;
            case 1:
                PlayerPrefs.SetInt("CharacterSelect", characterIndexSelect);
                characterSelectNum = 1;
                HeavyIsSelected = true;
                BanditIsSelected = false;
                character[1].color = Color.blue;
                character[0].color = Color.white;
                break;
        }
    }
    public void SelectButton(int characterIndex)
    {
        characterIndex = characterIndexSelect;
        if(characterIndexSelect<3)
        {
            characterIndexSelect++;
            if(characterIndexSelect==3)
            {
                characterIndexSelect = 0;
            }
        }
        switch(characterIndex)
        {
            case 0:
                //Bandit
                characterSelectNum = 1;
                BanditIsSelected = true;
                HeavyIsSelected = false;
                character[0].color = Color.blue;
                character[1].color = Color.white;
                break;
            case 1:
                //Heavy
                characterSelectNum = 1;
                HeavyIsSelected = true;
                BanditIsSelected = false;
                character[1].color = Color.blue;
                character[0].color = Color.white;
                break;
        }
        
    }
}
