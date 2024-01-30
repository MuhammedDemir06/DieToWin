using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Language
{
    Turkish, English, Japenese
}
public class LanguageControlPanel : MonoBehaviour
{
    public static LanguageControlPanel LanguageControlPaneInt;
    private Language languageController;
    public static bool TurkishLanguage, EnglishLanguage, JapeneseLanguage;
    [SerializeField] private int languageOfNumber;
    //Tick jawjdjwd
    [Tooltip("0=english/1=turkish/2=japenese")][SerializeField] private Image[] ticks; //0=english/1=turkish/2=japenese
    [SerializeField]private int LanguageOfNumberPublic;
    private void Awake()
    {
        // default language 
        languageOfNumber = 1;
        languageOfNumber = PlayerPrefs.GetInt("Language");
    }
    private void Start()
    {
        LanguageControlPaneInt = this;
    }
    //Data
    public void LanguageDataSystem()
    {
        languageOfNumber = LanguageOfNumberPublic;
    }
    private void Update()
    {
        switch (languageOfNumber)
        {
            case 0:
                ticks[0].gameObject.SetActive(true);
                ticks[1].gameObject.SetActive(false);
                ticks[2].gameObject.SetActive(false);
                languageController = Language.English;
                TurkishLanguage = false;
                EnglishLanguage = true;
                JapeneseLanguage = false;
                
                break;
            case 1:
                ticks[0].gameObject.SetActive(false);
                ticks[1].gameObject.SetActive(true);
                ticks[2].gameObject.SetActive(false);
                languageController = Language.Turkish;
                TurkishLanguage = true;
                EnglishLanguage = false;
                JapeneseLanguage = false;
                break;
            case 2:
                ticks[0].gameObject.SetActive(false);
                ticks[1].gameObject.SetActive(false);
                ticks[2].gameObject.SetActive(true);
                languageController = Language.Japenese;
                TurkishLanguage = false;
                EnglishLanguage = false;
                JapeneseLanguage = true;
                break;
        }
    }
    //Buttons
    public void EnglishLanguageButton()
    {
        languageOfNumber = 0;
        print("please restart game");
        PlayerPrefs.SetInt("Language", languageOfNumber);
    }
    public void TurkishLanguageButton()
    {  
        languageOfNumber = 1;
        print("please restart game");
        PlayerPrefs.SetInt("Language", languageOfNumber);
    }
    public void JapeneseLanguageButton()
    {
        languageOfNumber = 2;
        print("please restart game");
        PlayerPrefs.SetInt("Language", languageOfNumber);
    }
}
