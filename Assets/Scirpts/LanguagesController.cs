using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LanguagesController : MonoBehaviour
{
    [SerializeField] private string turkishVersion; 
    [SerializeField] private string englishVersion; 
    [SerializeField] private string japeneseVersion;
    [SerializeField] private Text text;

    private void Awake()
    {
        if (LanguageControlPanel.TurkishLanguage == true)
        {
            text.text = turkishVersion;
        }
        if (LanguageControlPanel.JapeneseLanguage == true)
        {
            text.text = japeneseVersion;
        }
        if (LanguageControlPanel.EnglishLanguage == true)
        {
            text.text = englishVersion;
        }
    }
    private void Update()
    {
        if (LanguageControlPanel.TurkishLanguage == true)
        {
            text.text = turkishVersion;
        }
        if (LanguageControlPanel.JapeneseLanguage == true)
        {
            text.text = japeneseVersion;
        }
        if (LanguageControlPanel.EnglishLanguage == true)
        {
            text.text = englishVersion;
        }
    }
}
