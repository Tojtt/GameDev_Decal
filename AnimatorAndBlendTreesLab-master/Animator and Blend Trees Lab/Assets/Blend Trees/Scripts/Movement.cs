using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("Speed");
            transform.position = (Vector2)transform.position + new Vector2(-5,0) * Time.deltaTime;
            sr.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Speed");
            transform.position = (Vector2)transform.position + new Vector2(5,0) * Time.deltaTime;
            sr.flipX = false;
        }
        else 
        {
            animator.SetTrigger("Chill");
        }

      

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
        }

    }
}
