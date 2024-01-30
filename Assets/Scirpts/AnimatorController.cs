using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimatorController : MonoBehaviour
{
    private Animator characterAnim;
    public static bool AttackAnimIsStart;
    [SerializeField] private Button attackButton;
    private void Start()
    {
       characterAnim = GetComponent<Animator>();
    }
    private void AnimationController()
    {
        if(CharacterController.IsRun)
           characterAnim.SetBool("Run", false);
        else
           characterAnim.SetBool("Run", true);

        if(GameManager.ManagerGame.ControlMode==Controller.Pc)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterAnim.SetTrigger("Jump");
            }
            if (Input.GetMouseButtonDown(0))
            {
                characterAnim.SetTrigger("Attack");
            }
        }
        switch(CharacterController.IsDead)
        {
            case true:
                characterAnim.SetBool("Death", true);
                break;
        }
        if (Input.GetKeyDown(KeyCode.M))
            characterAnim.SetTrigger("Hurt");

        if(AttackAnimIsStart)
        {
            attackButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            attackButton.GetComponent<Button>().enabled = true;
        }
    }
    private IEnumerator AttackSystem()
    {
        AttackAnimIsStart = true;
        characterAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.800f);
        AttackAnimIsStart = false;
        
    }
    private void Update()
    {
        AnimationController();
    }
    //Button
    public void JumpAnim()
    {
        characterAnim.SetTrigger("Jump");
    }
    public void AttackAnim()
    {
        StartCoroutine(AttackSystem());
    }
}
