using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public bool moving;
    public float moveTime = 2.0f;
	// Use this for initialization
	void Start () {
        moving = false;

	}
	
	// Update is called once per frame
	void Update () {
		// Don't use Update for this task
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Task 3: Start Your Coroutine Here
        if (collision.CompareTag("Player") && moving == false)
        {
            Vector2 currentPlayerPos = collision.transform.position;
            StartCoroutine(letGo(currentPlayerPos));
        }
        
    }

    IEnumerator letGo(Vector2 contactPoint)
    {
        moving = true;
        Vector2 currentPos = transform.position;
        float elapsedTime = 0.0f;
        while (elapsedTime < moveTime) {
            transform.position = Vector3.Lerp(currentPos, contactPoint, elapsedTime/moveTime);
            elapsedTime += Time.deltaTime;
        moving = false;
        yield return null;
        }  
    }
// Task 3: Write Your Coroutine Here
}
