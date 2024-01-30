using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private bool right,left;
    public static bool IsNotice;
    [SerializeField] private Animator circleBomb,dieAnim;
    public static float health;
    public static bool Damage;
    //health bar
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject healthCanvas;
    private void Start()
    {
        IsNotice = false;
        health = 2f;
        right = true;
        rb = GetComponent<Rigidbody2D>();
    }
    private void HealthController()
    {
        switch(IsNotice)
        {
            case true:
                healthCanvas.SetActive(true);
                break;
            case false:
                healthCanvas.SetActive(false);
                break;
        }
        if(health==0f)
        {
            StartCoroutine(DestroyTimer());
        }
        if(Damage)
            dieAnim.SetTrigger("Damage");
        if (health == 1)
            healthBar.fillAmount = 0.5f;
        if (health == 0)
            healthBar.fillAmount = 0f;

    }
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(0.800f);
        Destroy(gameObject);
    }
    private void Move()
    {
        switch(IsNotice)
        {
            case true:
                circleBomb.SetTrigger("Boom");
                break;
            case false:
               
                if (right == true)
                {
                    rb.velocity = Vector3.right * speed;
                }
                if (left == true)
                {
                    rb.velocity = Vector3.left * speed;
                }
                break;
        }
    }
    
    private void EnemyDirection()
    {
        var newScale = transform.localScale;
        switch(right)
        {
            case true:
                newScale.x = 0.7f;
                break;
            case false:
                newScale.x = -0.7f;
                break;
        }
        transform.localScale = newScale;
    }
    private void Update()
    {
        HealthController();
        Move();
    }
    private void FixedUpdate()
    {
        EnemyDirection();
    }
    //Triggers
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RightWall")
        {
            right = false;
            left = true;
        }
        if (collision.gameObject.tag == "LeftWall")
        {
            left = false;
            right = true;
        }
    }
}
