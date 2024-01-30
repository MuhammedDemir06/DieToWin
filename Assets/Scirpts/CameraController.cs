using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject heavy,bandit;
    [SerializeField] private float height;
    private void Start()
    {
        One.SaveCharacterAsset = PlayerPrefs.GetInt("SaveCharacterAsset");
    }
    private void Follow()
    {
        if (One.SaveCharacterAsset==2)
        {
            var newPos = new Vector3(heavy.transform.position.x, heavy.transform.position.y + height, -10f);
            transform.position = newPos;
        }
        if (One.SaveCharacterAsset==1)
        {
            var newPos = new Vector3(bandit.transform.position.x,bandit.transform.position.y + height, -10f);
            transform.position = newPos;
        }
    }
    private void Update()
    {
        Follow();
    }
}
