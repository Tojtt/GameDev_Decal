using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CameraControl : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How much time the camera is given to catch up to the player.")]
    private float m_SmoothTime = 0.75f;
    #endregion

    #region Private Variables
    private float p_Scale; //keeps the camera and player at a constant scale
    private float p_Dampen = 1; //how much distance the camera will maintain
    private Vector3 p_Velocity = Vector3.zero;
    private float p_MaxSpeed = 10; //fastest speed the camera can travel
    #endregion

    #region Cached Components
    private Vector3 cc_InitPos; //starting position of the camera
    private Transform cc_Tr; //transform of the camera
    #endregion

    #region Cached References
    private GameObject cr_Player;
    private Transform cr_PlTr; //transform of the player
    private Vector3 cr_PlayerPos;
    #endregion

    #region Initialization Methods
    private void Start()
    {
        cc_Tr = GetComponent<Transform>();
        cc_InitPos = cc_Tr.position;
        cr_Player = GameObject.Find("Player");
        cr_PlTr = cr_Player.GetComponent<Transform>();
        p_Scale = Screen.width / 723;
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        cr_PlayerPos = cr_PlTr.position;
        if (CheckPos() && cr_PlayerPos.x > -6)
        {
            //SmoothDamp creates a smooth moving camera and feels a bit more natural
            transform.position = Vector3.SmoothDamp(cc_InitPos, new Vector3(cr_PlayerPos.x, cc_InitPos.y, cc_InitPos.z), ref p_Velocity, m_SmoothTime * p_Scale, p_MaxSpeed);
            cc_InitPos = transform.position;
        }
    }
    #endregion

    #region Checker Methods
    //Checks if the camera should start moving based on its position and the variables scale and dampen
    private bool CheckPos()
    {
        return cr_PlayerPos.x > cc_InitPos.x + (p_Scale * p_Dampen) || cr_PlayerPos.x < cc_InitPos.x - (p_Scale * p_Dampen);
    }
    #endregion
}
