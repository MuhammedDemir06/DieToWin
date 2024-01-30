using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] private Material background;
    [SerializeField] private float speed;
    private void Move()
    {
        if(CharacterController.IsMove)
        {
            Vector3 offset = background.mainTextureOffset;
            if (CharacterController.InputX > 0)
            {
                offset.x += speed * Time.deltaTime;
            }
            else if (CharacterController.InputX < 0)
            {
                offset.x -= speed * Time.deltaTime;
            }
            background.mainTextureOffset = offset;
        }
    }
    private void Update()
    {
        Move();
    }
}
