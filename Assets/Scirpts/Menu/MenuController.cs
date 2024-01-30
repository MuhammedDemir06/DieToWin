using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuController : MonoBehaviour
{
    //Menu UI
    [SerializeField] private GameObject menuButtons, menuLevels,options,storePanel;
    //Effects
    [SerializeField] private GameObject transition;
    [SerializeField] private Animator transitionEffect;
    private int sceneNumber;
    [SerializeField] private AudioSource buttonSound;
    //Level System
    private int levelPassed;
    [SerializeField] private Button[] levelButton;
    [SerializeField] private GameObject[] levelLocks;
    //Coin System
    public int CoinNumber;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        GameManager.CoinNumberInGame=PlayerPrefs.GetInt("CoinNumberInGame");
        CoinNumber =PlayerPrefs.GetInt("CoinNumber");
        print(GameManager.CoinNumberInGame);
        CoinNumber = GameManager.CoinNumberInGame + CoinNumber;
        buttonSound.Pause();
    }
    private void Start()
    {
        PurchaseSystem.characterSelectNum = 0;
        PlayerPrefs.SetInt("CoinNumber", CoinNumber);
        LevelButtonControl();
        StartCoroutine(TransitionEffectTimer());
    }
    private void LevelButtonControl()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        for (int i = 0; i < levelButton.Length; i++)
        {
            levelButton[i].interactable = i <= levelPassed;
           if(i<levelPassed)
           {
                levelLocks[i].SetActive(false);
           }
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
    private void Update()
    {
        coinText.text = CoinNumber.ToString();
    }
    //Button Controller
    public void NextLevel(int sceneIndex)
    {
        buttonSound.Play();
        sceneNumber = sceneIndex;
        StartCoroutine(TransitionFinal());
    }
    public void PlayButtons()
    {
        if(PurchaseSystem.characterSelectNum!=1)
        {
            PurchaseSystem.BanditIsSelected = true;
        }
        PlayerPrefs.SetInt("CoinNumber", CoinNumber);
        buttonSound.Play();
        menuButtons.SetActive(false);
        menuLevels.SetActive(true);
    }
    public void BackButton()
    {
        buttonSound.Play();
        menuButtons.SetActive(true);
        menuLevels.SetActive(false);
        options.SetActive(false);
        storePanel.SetActive(false);
    }
    public void OptionsButton()
    {
        buttonSound.Play();
        menuButtons.SetActive(false);
        options.SetActive(true);
    }
    public void QuitButton()
    {
        buttonSound.Play();
        Application.Quit();
    }
    public void StoreButton()
    {
        buttonSound.Play();
        menuButtons.SetActive(false);
        menuButtons.SetActive(false);
        storePanel.SetActive(true);
    }
}
