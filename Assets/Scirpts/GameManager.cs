using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum Controller
{
    Pc,Mobile
}
public class GameManager : MonoBehaviour
{
    public static GameManager ManagerGame;
    public Controller ControlMode;
    //Coin Controller
    [SerializeField] private int coinNumber;
    [SerializeField] private TextMeshProUGUI coinNumberText,coinLostResultText, coinWinResultText;
    public static int CoinNumberInGame;  //Nott
    //Character UI && Level Uı Controller
    [SerializeField] private GameObject characterUIBandit,characterUIHeavy, pausedUI, winUI, lostUI;
    [SerializeField] private Animator backgroundAnim;
    public static bool IsWonLevel;
    //Sound System
    [SerializeField] private AudioSource LevelSound;
    [SerializeField] private bool isOpenSound;
    [SerializeField] private Text SoundControlText;
    [SerializeField] private GameObject soundOfDeath;
    [SerializeField] private AudioSource buttonSound;
    //Effect 
    [SerializeField] private GameObject transition;
    [SerializeField] private Animator transitionEffect;
    private int sceneNumber;
    //Level
    private int sceneIndex, levelPassed;
    public static int SceneIndexPublic;
    [Header("Character")]
    [SerializeField] private GameObject bandit, heavy;

    private void Awake()
    {
        buttonSound.Pause();
        StartCoroutine(TransitionEffectTimer());
        One.SaveCharacterAsset = PlayerPrefs.GetInt("SaveCharacterAsset");
    }
    private void Start()
    {
        CharacterSpawn();

        CoinNumberInGame = 0;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");

        isOpenSound = true;
        IsWonLevel = false;
        ManagerGame = this;
        coinNumber = 0;
    }
    private void CharacterSpawn()
    {
        if(One.SaveCharacterAsset==1)
        {
            print("bandit");
            characterUIBandit.SetActive(true);
            characterUIHeavy.SetActive(false);
            bandit.SetActive(true);
            heavy.SetActive(false);
        }
        if(One.SaveCharacterAsset==2)
        {
            print("Heavy");
            characterUIBandit.SetActive(false);
            characterUIHeavy.SetActive(true);
            heavy.SetActive(true);
            bandit.SetActive(false);
        }
    }
    private IEnumerator TransitionEffectTimer()
    {
        transitionEffect.SetTrigger("Start");
        transition.SetActive(true);
        yield return new WaitForSeconds(2.517f);
        transition.SetActive(false);
    }
    private IEnumerator TransitionFinal()
    {
        transition.SetActive(true);
        transitionEffect.SetTrigger("Final");
        yield return new WaitForSeconds(2.000f);
        SceneManager.LoadScene(sceneNumber);
    }

    public void CoinUpdate()
    {
        coinNumber++;
        
    }
    private void LostController()
    {
        if(CharacterController.IsDead==true)
        {
            soundOfDeath.SetActive(true);
            winUI.SetActive(false);
            coinLostResultText.text = coinNumber.ToString();
            characterUIHeavy.SetActive(false);
            characterUIBandit.SetActive(false);
            pausedUI.SetActive(false);
            lostUI.SetActive(true);
            backgroundAnim.SetBool("IsPaused", true);
        }
    }
    private void WinController()
    {
        switch(IsWonLevel)
        {
            case true:
                winUI.SetActive(false);
                coinWinResultText.text = coinNumber.ToString();
                characterUIHeavy.SetActive(false);
                characterUIBandit.SetActive(false);
                pausedUI.SetActive(false);
                winUI.SetActive(true);
                backgroundAnim.SetBool("IsPaused", true);
                break;
        }
    }
    private void SoundController()
    {
        switch (isOpenSound)
        {
            case true:
                LevelSound.gameObject.SetActive(true);
                if (LanguageControlPanel.EnglishLanguage == true)
                    SoundControlText.text = "Sound ON";
                if (LanguageControlPanel.TurkishLanguage == true)
                    SoundControlText.text = "Ses Açık";
                if (LanguageControlPanel.JapeneseLanguage == true)
                    SoundControlText.text = "サウンドオン";
                break;
            case false:
                LevelSound.gameObject.SetActive(false);
                if (LanguageControlPanel.EnglishLanguage == true)
                    SoundControlText.text = "Sound OFF";
                if (LanguageControlPanel.TurkishLanguage == true)
                    SoundControlText.text = "Ses Kapalı";
                if (LanguageControlPanel.JapeneseLanguage == true)
                    SoundControlText.text = "サウンドオフ ";
                break;
        }
    }
    //level Controller
    private void LoadMenu()
    {
        sceneNumber = 0;
        StartCoroutine(TransitionFinal());
    }
    private void Update()
    {
        CoinNumberInGame = coinNumber;
        PlayerPrefs.SetInt("CoinNumberInGame",CoinNumberInGame);
        //Coin Save
        SceneIndexPublic = sceneIndex;

        // CharacterSpawn();
       // AdsManager();
        SoundController();
        WinController();
        LostController(); 
        coinNumberText.text = coinNumber.ToString();
    }
    private IEnumerator LastGame()
    {
        characterUIHeavy.SetActive(false);
        characterUIBandit.SetActive(false);
        yield return new WaitForSeconds(5f);
        LoadMenu();
    }
    // Level UI Buttons
    public void NextLevelCheck(int newSceneNum)
    {
        buttonSound.Play();
        if (sceneIndex == 5)
        {
           if(LastGameEffect.LastGameEffectNum==0)
                StartCoroutine(LastGame());
            if (LastGameEffect.LastGameEffectNum >0)
                LoadMenu();
        }
        else
        {
            if (levelPassed < sceneIndex)
            {
                levelPassed = sceneIndex;
                PlayerPrefs.SetInt("LevelPassed", levelPassed);
            }
            // LoadNextLevel();
            //Next Level
            sceneNumber = newSceneNum;
            StartCoroutine(TransitionFinal());
        }
       
    }
    public void SoundButton()
    {
        buttonSound.Play();
        isOpenSound = !isOpenSound;
    }
    public void PauseButton()
    {
        buttonSound.Play();
        characterUIHeavy.SetActive(false);
        characterUIBandit.SetActive(false);
        pausedUI.SetActive(true);
        backgroundAnim.SetBool("IsPaused", true);   
    }
    public void ContinueButton()
    {
        buttonSound.Play();
        characterUIHeavy.SetActive(true);
        characterUIBandit.SetActive(true);
        backgroundAnim.SetBool("IsPaused", false);
    }
    public void MenuButton()
    {
        buttonSound.Play();
        sceneNumber = 0;
        StartCoroutine(TransitionFinal());
    }
    public void RestartButton(int number)
    {
        buttonSound.Play();
        // CharacterController.IsDead = false;
        sceneNumber = number;
        StartCoroutine(TransitionFinal());
    }

    //[UnityEditor.MenuItem(itemName: "Game/delete All Data")]
    //public static void DeleteData()
    //{
    //    LanguageControlPanel.LanguageControlPaneInt.LanguageDataSystem();
    //    PurchaseSystem.PurchaseInterface.Data();
    //    LastGameEffect.LastGameEffectInterface.DeleteData();
    //    PlayerPrefs.DeleteAll();
    //}
}
