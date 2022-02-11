using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    #region Movment_variables
    public float movespeed;
    private float movespeed_current;
    float x_input;
    float y_input;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Attack_variables
    public float Damage;
    public float attackspeed = 1;
    public float abilityCoolDown = 15;
    public float hitsize = 8;
    float currCoolDown;
    float attackTimer;
    float speedTimer;
    public float hitboxtiming;
    public float endanimationtiming;
    bool isAttacking;
    Vector2 currDirection;
    #endregion

    #region Animation_components
    Animator anim;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    public Slider HPSlider;
    public Slider CoolDownSlider;
    #endregion

    #region Unity_functions

    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        attackTimer = 0;
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        currCoolDown = abilityCoolDown;
        HPSlider.value = currHealth / maxHealth;
    }

    private void Update()
    {
        currCoolDown += Time.deltaTime;
        CoolDownSlider.value = currCoolDown / abilityCoolDown;
        
        if (isAttacking)
        {
            return;
        }
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        Move();

        if (Input.GetKeyDown(KeyCode.J) && attackTimer <= 0)
        {
            Attack();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currCoolDown >= abilityCoolDown)
        {
            Kamehameha();
        }
        else
        {
            currCoolDown += Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            Interact();
        }

        if(speedTimer <= 0)
        {
            movespeed_current = movespeed;
        }
        speedTimer -= Time.deltaTime;

    }
    #endregion

    #region Movment_functions

    private void Move()
    {
        anim.SetBool("Moving", true);
        if (x_input > 0)
        {
            PlayerRB.velocity = Vector2.right * movespeed_current;
            currDirection = Vector2.right;
        }
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left * movespeed_current;
            currDirection = Vector2.left;
        }

        else if (y_input > 0)
        {
            PlayerRB.velocity = Vector2.up * movespeed_current;
            currDirection = Vector2.up;
        }
        else if (y_input < 0)
        {
            PlayerRB.velocity = Vector2.down * movespeed_current;
            currDirection = Vector2.down;
        }
        else
        {
            PlayerRB.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
        }
        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);

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
    private void Kamehameha()
    {
        currCoolDown = 0;
        //handles animation and hit boxes
        StartCoroutine(KamehamehaRoutine());
    }

    private void hitbox(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Tons of Damage");
                hit.transform.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
    }
    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PlayerRB.velocity = Vector2.zero;

        anim.SetTrigger("Attacking");

        //start sound effect
        FindObjectOfType<AudioManager>().Play("PlayerAttack");

        yield return new WaitForSeconds(hitboxtiming);
        Debug.Log("Casting hitbox now");

        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero);
        
        

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Tons of Damage");
                hit.transform.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }

        yield return new WaitForSeconds(hitboxtiming);
        isAttacking = false;

        yield return null;
    }

    IEnumerator KamehamehaRoutine()
    {
        isAttacking = true;
        PlayerRB.velocity = Vector2.zero;

        anim.SetTrigger("Kamehameha");

        //start sound effect
        FindObjectOfType<AudioManager>().Play("Kamehameha");

        yield return new WaitForSeconds(hitboxtiming);
        Debug.Log("Casting hitbox now");
        if((currDirection == Vector2.left) || (currDirection == Vector2.right))
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, new Vector2(hitsize,1), 0f, Vector2.zero);
            hitbox(hits);

        } 
        else 
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, new Vector2(1,hitsize) , 0f, Vector2.zero);
            hitbox(hits);
        }
        

        yield return new WaitForSeconds(hitboxtiming);
        isAttacking = false;

        yield return null;
    }
    #endregion

    #region Item_functions
    //take danmage based on value param passed in by caller
    public void TakeDamage(float value)
    {
        //call sound effect
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        //decrement health
        currHealth -= value;
        Debug.Log("Health is now " + currHealth.ToString());

        //change UI
        HPSlider.value = currHealth / maxHealth;

        //check if dead
        if (currHealth <= 0)
        {
            Die();
        }
    }

    //heals player based on value param passed in by caller
    public void Heal(float value)
    {
        //icrement health value
        currHealth += value;
        currHealth = Mathf.Min(currHealth, maxHealth);
        Debug.Log("Health is now " + currHealth.ToString());
        HPSlider.value = currHealth / maxHealth;
    }

    public void Speed(float value)
    {
        //icrement health value
        movespeed_current = movespeed*value;
        Debug.Log("Speed is now " + movespeed.ToString());
        speedTimer = 4;
        
    }

    //destroys palyer object and triggers end scene stuff
    private void Die()
    {
        //call sound effect
        FindObjectOfType<AudioManager>().Play("PlayerDeath");

        //destroy this object
        Destroy(this.gameObject);

        //trigger anything to end game, find GameManager and lose game
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().LoseGame();
    }
    #endregion

    #region Interact_functions

    private void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero, 0f);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.transform.CompareTag("Chest"))
            {
                hit.transform.GetComponent<Chest>().Interact();
            }
        }
    }
    #endregion

}