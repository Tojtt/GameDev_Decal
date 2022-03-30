using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Candy : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The UI element that displays the number of candies.")]
    private Text m_CandyText;
    #endregion

    #region Cached References
    GameObject cr_Player;
    PlayerController cr_Controller;
    #endregion

    #region Initialization Methods
    private void Start ()
    {
        cr_Player = GameObject.Find("Player");
        cr_Controller = cr_Player.GetComponent<PlayerController>();
	}
    #endregion

    #region Main Updates
    private void Update ()
    {
        //Continually updates the UI text for the number of candies collected
        m_CandyText.text = "Candies: " + cr_Controller.NumCandies.ToString();
	}
    #endregion
}
