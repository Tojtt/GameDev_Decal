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
    
    public GameObject pauseScreen;
    private bool p_IsPaused = false;
    
	#endregion

	#region Private Variables
    private string p_SceneName;
	#endregion

	#region Initialization Methods
	private void Awake()
    {

    }
    
	private void Start ()
    {
        m_Player = GameObject.Find("Player");
        //GetActiveScene() returns a scene object
        p_SceneName = SceneManager.GetActiveScene().name;
	}
    #endregion

    #region Main Updates
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if(Input.GetKeyDown(KeyCode.P) && !p_SceneName.Equals("Menu"))
        {
            Pause();
        }

	}

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(p_SceneName);
    }

    public void GoToScene(string name)
    {
            SceneManager.LoadSceneAsync(name);
    }

    #endregion
}
