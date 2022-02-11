using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    #region GameObject_variables
    [SerializeField]
    [Tooltip("health pack")]
    private GameObject healthpack;
    [SerializeField]
    [Tooltip("speed pack")]
    private GameObject speedpack;
    #endregion

    #region Chest_functions

    IEnumerator DestroyChest()
    {
        yield return new WaitForSeconds(.3f);
        float val = Random.Range(0f, 2f);
        Debug.Log(val);
        
        if(val < 1)
        {
            Instantiate(healthpack, transform.position, transform.rotation);
        } else
        {
            Instantiate(speedpack, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }
    
    public void Interact()
    {
        StartCoroutine("DestroyChest");
    }

    #endregion
}
