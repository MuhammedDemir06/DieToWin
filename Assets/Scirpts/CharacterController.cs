using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterController : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager managerGame;
    //Controller
    private PolygonCollider2D boxColl;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float jumpPower;
    public static float InputX;
    private float inputX;
    [SerializeField] private LayerMask groundLayer;
    public static bool IsRun;
    public static bool IsMove;
    public static bool IsDead;
    private bool attackPos;
    //health 
    [SerializeField] private Image[] health;
    // Sound
    [SerializeField] private AudioSource coinSound;
    public static int HeartCounter;
    //Ad
    [SerializeField] private AdManager adManager;
    private void Start()
    {
        HeartCounter = 0;
        coinSound.Stop();
        IsDead = false;
        IsMove = true;
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<PolygonCollider2D>();
    }
    private void HealthController()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            HeartCounter++;
        }
        switch (HeartCounter)
        {
            case 1:
                health[0].color = Color.black;
                break;
            case 2:
                health[1].color = Color.black;
                break;
            case 3:
                print("you lost");
                IsDead = true;
                health[2].color = Color.black;
                break;
        }
    }
    private void GetInput()
    {
        if (GameManager.ManagerGame.ControlMode == Controller.Pc)
        {
            inputX = Input.GetAxis("Horizontal");
        }
    }
    private void Move()
    {
        print(IsDead);
        InputX = inputX;
        // move X 
        if (IsGround())
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        // move Y (Jump)
        if (GameManager.ManagerGame.ControlMode == Controller.Pc)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGround())
                {
                    jumpPower = 6f;
                    rb.velocity = Vector2.up * jumpPower;
                }
                else
                {
                    jumpPower = 0f;
                }
            }
        }
    }
    private bool IsGround()
    {
        return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, 1f, groundLayer);
    }
    private void CharacterDirection()
    {
        var newScale = transform.localScale;
        if (inputX > 0)
        {
            IsRun = true;
            newScale.x = -1.5f;
        }
        if (inputX < 0)
        {
            IsRun = true;
            newScale.x = 1.5f;
        }
        if (inputX == 0)
        {
            IsRun = false;
        }
        transform.localScale = newScale;
    }
    private void Update()
    {
        HealthController();
        GetInput();
        Move();
        CharacterDirection();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Thorns")
        {
            HeartCounter = 3;
            health[0].color = Color.black;
            health[1].color = Color.black;
            health[2].color = Color.black;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            IsMove = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Is notice Enemy
        if (collision.gameObject.tag == "BoundryEnemy")
        {
            EnemyController.IsNotice = true;
            print("Inside the Field");
        }
        if (collision.gameObject.tag == "Enemy")
        {
            print("++");
            attackPos = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BoundryEnemy")
        {
            EnemyController.IsNotice = false;
            print("Outside the Field");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            IsMove = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LevelIsOver")
        {
            GameManager.IsWonLevel = true;
            adManager.Ads();
        }
        if (collision.gameObject.tag == "Coin")
        {
            GameManager.CoinNumberInGame++;
            coinSound.Play();
            managerGame.CoinUpdate();
            Destroy(collision.gameObject);
        }

    }

    //Button Controller
    public void MoveXController(float input)
    {
        inputX = input;
    }
    public void Jump()
    {
        if (IsGround())
        {
            jumpPower = 6f;
            rb.velocity = Vector2.up * jumpPower;
        }
        else
        {
            jumpPower = 0f;
        }
    }
    public void AttackButton()
    {
        switch (attackPos)
        {
            case true:
                if (AnimatorController.AttackAnimIsStart == true)
                {
                    EnemyController.health -= 1f;
                    EnemyController.Damage = true;
                }
                else
                    EnemyController.Damage = false;

                break;
        }
    }
}
