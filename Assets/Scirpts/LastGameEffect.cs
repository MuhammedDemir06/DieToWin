using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGameEffect : MonoBehaviour
{
    public static LastGameEffect LastGameEffectInterface;
    //last Game
    public static int LastGameEffectNum;
    [SerializeField] private GameObject lastGameEffect;
    private void Start()
    {
        LastGameEffectNum = PlayerPrefs.GetInt("LastGameEffectNum");
        LastGameEffectInterface = this;
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
    private IEnumerator LastGame()
    {
        lastGameEffect.SetActive(true);
        yield return new WaitForSeconds(5f);
    }
    public void NextLevelLastGameEffect()
    {
       if(GameManager.SceneIndexPublic==5)
       {
            StartCoroutine(LastGame());
       }
        if (LastGameEffectNum < 2)
        {
            LastGameEffectNum++;
        }
        PlayerPrefs.SetInt("LastGameEffectNum", LastGameEffectNum);
    }
}
