using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject sentry;
    public Transform[] spawnPositions;
	public float waitTime = 3.0f;

	// Use this for initialization
	void Start () {
        // Task 2: Start Your Coroutine Here
		StartCoroutine(SpawnEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		// Don't use Update for this task!
	}

    // Task 2: Write Your Coroutine Here
	IEnumerator SpawnEnemy()
    {
		foreach(Transform pos in spawnPositions)
		{
			Instantiate(sentry, pos.position, Quaternion.identity);
			yield return new WaitForSeconds(5);

		}
        
    
    }

}
