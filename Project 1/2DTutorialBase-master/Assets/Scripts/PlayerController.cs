using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    #region Movment_variables
    public float movespeed;
    float x_input;
    float y_input; 
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Attack_variables
    public float Damage; 
    public float attackspeed = 1;
    float attackTimer;
    public float hitboxtiming;
    public float endanimationtiming;
    bool isAttacking;
    Vector2 currDirection;
    #endregion

    #region Unity_functions

    private void Awake() 
    {
        PlayerRB = GetComponent<Rigidbody2D>();     
        attackTimer = 0;
    }

    private void Update() 
    {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        Move();

        if(Input.GetKeyDown(KeyCode.J) && attackTimer <= 0) 
        {
            Attack();
        } else 
        {
            attackTimer -= Time.deltaTime;
        }
    }
    #endregion

    #region Movment_functions

    private void Move()
    {
        if(x_input > 0)
        {
            PlayerRB.velocity = Vector2.right * movespeed;
            currDirection = Vector2.right;
        } 
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left;
        }

        else if (y_input > 0)
        {
            PlayerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
        } 
        else if (y_input < 0)
        {
            PlayerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down;
        }
        else
        {
            PlayerRB.velocity = Vector2.zero;
        }

    }
    #endregion

    #region Attack_functions
    private void Attack()
    {
        Debug.Log("attacking now");
        Debug.Log(currDirection);
        attackTimer = attackspeed;
        //handles animation and hit boxes
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true; 
        PlayerRB.velocity = Vector2.zero;
        yield return new WaitForSeconds(hitboxtiming);
        Debug.Log("Casting hitbox now"); 
        
        yield return null;
    }
    #endregion
}
