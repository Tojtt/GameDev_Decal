using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SceneController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("A place to keep the default player object in the level. If a player object already exists, delete this one.")]
    private GameObject m_Player;
	#endregion

	#region Private Variables
	#endregion

	#region Initialization Methods
	private void Awake()
    {

    }
    
	private void Start ()
    {
        m_Player = GameObject.Find("Player");
	}
    #endregion

    #region Main Updates
    private void Update ()
    {

	}
    #endregion
}
