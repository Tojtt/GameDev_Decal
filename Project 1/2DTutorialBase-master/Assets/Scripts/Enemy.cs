using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Movment_variables
    public float movespeed;
    #endregion

    #region Physics_components
    Rigidbody2D EnemyRB;
    #endregion

    #region Targeting_variables
    public Transform player;
    #endregion

    #region Attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    #endregion


    #region Unity_functions
    //run once on creation
    private void Awake() 
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }
    //run once every frame
    private void Update() 
    {
        //check to see if we know where player is
        if(player == null)
        {
            return;
        }

        Move();
    }
    #endregion

    #region Movment_functions
    //move directly at player
    private void Move()
    {
        //calculate movment vector: player position - enemy position = diction of player relative to enemy
        Vector2 direction = player.position - transform.position;
        EnemyRB.velocity = direction.normalized * movespeed;

    }
    #endregion

    #region Attack_functions
    
    // Raycasts box for player, casus damage, spawns explosion prefab
    private void Explode()
    {
        //call sound effect
        FindObjectOfType<AudioManager>().Play("Explosion");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if(hit.transform.CompareTag("Player"))
            {
                //Cause damage
                Debug.Log("Hit Player with Explosion");

                //spawn explosion
                Instantiate(explosionObj, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);
                
            }
        }
        
        Destroy(this.gameObject);  
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
    #endregion

    #region Health_functions

    //enemy takes damage based on value param
    public void TakeDamage(float value)
    {
        //call sound effect
        FindObjectOfType<AudioManager>().Play("BatHurt");
        
        //decrement health
        currHealth -= value;
        Debug.Log("Health is now " + currHealth.ToString());

        //check for death
        if(currHealth <= 0)
        {
            Die();
        }

    }
    private void Die()
    {
        //destroy this game object
        Destroy(this.gameObject);
    }
    #endregion
}
