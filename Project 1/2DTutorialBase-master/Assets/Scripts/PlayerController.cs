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
    float attackspeed = 1;
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
    }

    private void Update() 
    {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        Move();

        if(Input.GetKeyDown(KeyCode.J)) 
        {
            Attack();
        }
    }
    #endregion

    #region Movment_functions

    private void Move()
    {
        if(x_input > 0)
        {
            PlayerRB.velocity = Vector2.right * movespeed;
        } 
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left * movespeed;
        }

        else if (y_input > 0)
        {
            PlayerRB.velocity = Vector2.up * movespeed;
        } 
        else if (y_input < 0)
        {
            PlayerRB.velocity = Vector2.down * movespeed;
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
    }

    IEnumerator AttackRoutine()
    {
        yield return null;
    }
    #endregion
}
