using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBomb : MonoBehaviour
{
    
    //trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            CharacterController.HeartCounter+=1;
           // Destroy(gameObject);
        }
    }
}
