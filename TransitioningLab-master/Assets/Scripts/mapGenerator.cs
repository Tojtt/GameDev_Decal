using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MapGenerator : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The prefab to use as a background that never ends no matter how far the player runs.")]
    private GameObject m_MapPiece;

    [SerializeField]
    [Tooltip("The prefab of the candy that will be spawning throughout the level.")]
    private GameObject m_CandyPrefab;
    #endregion

    #region Private Variables
    private GameObject p_PrevMap;
    private GameObject p_CurrMap;
    private GameObject m_NextMap;

    private GameObject p_CurrCandy;
    #endregion

    #region Cached References
    private GameObject cr_Player;
    private Transform cr_PlayerTR;
    private Vector2 cr_PlayerPos;
    #endregion

    #region Initialization Methods
    private void Awake()
    {
        cr_Player = GameObject.Find("Player");

        //creates the first 3 platforms for the player
        p_PrevMap = Instantiate(m_MapPiece, new Vector3(-36, 0), new Quaternion());
        p_CurrMap = Instantiate(m_MapPiece, new Vector3(0, 0), new Quaternion());
        m_NextMap = Instantiate(m_MapPiece, new Vector3(36, 0), new Quaternion());

        //creates 5 candies at pseudorandom and increasingly further distances with random color
        for (int x = 1; x < 6; x++)
        {
            p_CurrCandy = Instantiate(m_CandyPrefab, new Vector2(x * 5 * Random.Range(0.0f, 1.0f), -1.4f), new Quaternion());
            p_CurrCandy.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }
    
	private void Start () {
        cr_PlayerTR = cr_Player.GetComponent<Transform>();
        cr_PlayerPos = cr_PlayerTR.position;
	}
    #endregion

    #region Main Updates
    void Update()
    {
        cr_PlayerPos = cr_PlayerTR.position;
        ModifyMap();
        SpawnCandy();
    }
    #endregion

    #region Map Update Methods
    //checks if it is necessary to spawn/despawn pieces of the map based on the position of the player's transform
    private void ModifyMap()
    {
        float currMapXPos = p_CurrMap.GetComponent<Transform>().position.x;
        if (cr_PlayerPos.x > currMapXPos + 36)
        {
            Destroy(p_PrevMap);
            p_PrevMap = p_CurrMap;
            p_CurrMap = m_NextMap;
            m_NextMap = Instantiate(m_MapPiece, new Vector3(currMapXPos + 72, 0), new Quaternion());
        }
        else if (cr_PlayerPos.x < currMapXPos - 36)
        {
            Destroy(m_NextMap);
            m_NextMap = p_CurrMap;
            p_CurrMap = p_PrevMap;
            p_PrevMap = Instantiate(m_MapPiece, new Vector3(currMapXPos - 72, 0), new Quaternion());
        }
    }

    //checks if more candies need to be spawned based on the distance between the last spawned candy and the player
    private void SpawnCandy()
    {
        if (p_CurrCandy)
        {
            float currCandyXPos = p_CurrCandy.GetComponent<Transform>().position.x;
            if (cr_PlayerPos.x > currCandyXPos - 10)
            {
                p_CurrCandy = Instantiate(m_CandyPrefab, new Vector2(cr_PlayerPos.x + 10 + (10 * Random.Range(-1.0f, 1.0f)), -1.4f), new Quaternion());
                p_CurrCandy.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            }
        }
        else
        {
            p_CurrCandy = Instantiate(m_CandyPrefab, new Vector2(cr_PlayerPos.x + 10 + (7 * Random.Range(-1.0f, 1.0f)), -1.4f), new Quaternion());
            p_CurrCandy.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }
    #endregion
}
