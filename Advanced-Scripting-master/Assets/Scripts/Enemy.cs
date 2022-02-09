using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float horizontalBound = 10;
    float verticalBound = 4;
    float elapsedTime = 0;

    [SerializeField]
    [Tooltip("radius that control the pattern that the enemy is moving.")]
    private float radius;
    void Start()
    {
        transform.position = new Vector2(Random.Range(-horizontalBound, horizontalBound),Random.Range(-verticalBound, verticalBound));
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 startingPosition = transform.position;
        elapsedTime = elapsedTime + Time.deltaTime;
        transform.position = startingPosition + new Vector2(radius * Mathf.Sin( elapsedTime), radius * Mathf.Sin(elapsedTime));

    }
}
