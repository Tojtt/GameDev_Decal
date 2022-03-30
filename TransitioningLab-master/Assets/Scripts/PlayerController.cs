using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How quickly the player can move around.")]
    private int m_Speed;
    #endregion

    #region Private Variables
    // The amount of candy that the player currently has
    private int p_NumCandies;
    #endregion

    #region Cached Components
    private Rigidbody2D cc_Rb;
    #endregion

    #region Initialization Methods
    private void Start()
    {
        cc_Rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Main Updates
    void Update () {
        float vel = Input.GetAxis("Horizontal");
        cc_Rb.velocity = new Vector2(vel * m_Speed, 0);
	}
    #endregion

    #region Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //increase candy count and destroy the candy when the player collides with a candy
        if (collision.gameObject.tag.Equals("candy"))
        {
            p_NumCandies += 1;
            Destroy(collision.gameObject);
        }
    }
    #endregion

    #region Accessors and Mutators
    public int NumCandies
    {
        get { return p_NumCandies; }
        set { p_NumCandies = value; }
    }
    #endregion
}
