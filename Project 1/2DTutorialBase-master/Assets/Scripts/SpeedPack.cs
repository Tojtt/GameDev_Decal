using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPack : MonoBehaviour
{
    #region SpeedPack_variables
    [SerializeField]
    [Tooltip("the amount the player heals")]
    private float multiplier;
    #endregion

    #region Heal_functions

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().Speed(multiplier);
            Destroy(this.gameObject);
        }
    }

    #endregion
}
